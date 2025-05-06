using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MatchGoalAPI.Services.Repositories
{
	public class AuthRepository : IAuthRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly ILogger<AuthRepository> _logger;
		public AuthRepository(ILogger<AuthRepository> logger,RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
			_roleManager = roleManager;
			_logger = logger;
		}
		public async Task<AuthDto> RegisterAsync(RegisterUserDto registerUserDto)
		{
			if (await _userManager.FindByEmailAsync(registerUserDto.Email) != null)
				return new AuthDto { Message = "Email is already registered!" };
			if (await _userManager.FindByNameAsync(registerUserDto.UserName) != null)
				return new AuthDto { Message = "User Name is already registered!" };

			ApplicationUser user = new ApplicationUser
			{
				UserName = registerUserDto.UserName,
				Email = registerUserDto.Email,
			};
			
			IdentityResult result = await _userManager.CreateAsync(user,registerUserDto.Password);

			if (!result.Succeeded)
			{
				var errors = string.Empty;

				foreach (var error in result.Errors)
				{
					errors += $"{error.Description},";
				}

				return new AuthDto { Message = errors };
			}

			await _userManager.AddToRoleAsync(user,"User");

			string token = GenerateJwtToken(user).Result;

			var refreshToken = GenerateRefreshToken();
			user.RefreshTokens?.Add(refreshToken);
			await _userManager.UpdateAsync(user);

			return new AuthDto
			{ 
				Email = user.Email,
				UserName = user.UserName,
				IsAuthenticated = true,
				Token = token,
				RefreshToken = refreshToken.Token,
				RefreshTokenExpiretion = refreshToken.ExpiresOn,
				Roles = new List<string> { "User" },
				Message = "User registered successfully"
			};
		}
		public async Task<string> AddToRoleAsync(AddToRoleDto addRoleModel)
		{
			ApplicationUser user = await _userManager.FindByIdAsync(addRoleModel.UserID);

			if (user == null || !await _roleManager.RoleExistsAsync(addRoleModel.Role))
				return "Invalid user ID or Role";
			if (await _userManager.IsInRoleAsync(user,addRoleModel.Role))
				return "User already assigned to this role";

			var result = await _userManager.AddToRoleAsync(user, addRoleModel.Role);

			return result.Succeeded ? "Role added successfully" : "Failed to add role";
		}
		public async Task<AuthDto> GetTokenAsync(LoginUserDto loginUserDto)
		{
			AuthDto authModel = new AuthDto();

			ApplicationUser user = await _userManager.FindByEmailAsync(loginUserDto.Email);
			if (user == null)
			{
				authModel.Message = "Email is incorrect!";
				return authModel;
			}
			bool result = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
			if (!result)
			{
				authModel.Message = "Password is incorrect!";
				return authModel;
			}

			var token = GenerateJwtToken(user).Result;
			IList<string> roles = await _userManager.GetRolesAsync(user);
			
			authModel.Token = token;
			authModel.IsAuthenticated = true;
			authModel.UserName = user.UserName;
			authModel.Email = user.Email;
			authModel.Roles = roles.ToList();

			if (user.RefreshTokens.Any(t => t.IsActive))
			{
				var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
				authModel.RefreshToken = activeRefreshToken.Token;
				authModel.RefreshTokenExpiretion = activeRefreshToken.ExpiresOn;
			}
			else
			{
				RefreshToken refreshToken = GenerateRefreshToken();
				authModel.RefreshToken = refreshToken.Token;
				authModel.RefreshTokenExpiretion = refreshToken.ExpiresOn;
				user.RefreshTokens.Add(refreshToken);
				await _userManager.UpdateAsync(user);
			}

			return authModel;
		}
		private RefreshToken GenerateRefreshToken()
		{
			byte[] randomNumber = new byte[32];
			using var generator = new RNGCryptoServiceProvider();

			generator.GetBytes(randomNumber);

			return new RefreshToken
			{
				Token = Convert.ToBase64String(randomNumber),
				ExpiresOn = DateTime.UtcNow.AddDays(10),
				CreatedOn = DateTime.UtcNow
			};
		}
		private async Task<string> GenerateJwtToken(ApplicationUser user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();

			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Email, user.Email),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim("uid", user.Id)
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken token = new JwtSecurityToken(
			issuer: _configuration["Jwt:Issuer"],
			audience: _configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddMinutes(_configuration.GetValue<int>("Jwt:ExpireTimeByMinutes")),
				signingCredentials: signingCredentials
				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		public async Task<AuthDto> RefreshTokenAsync(string token)
		{
			AuthDto authModel = new AuthDto();

			ApplicationUser user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

			if (user == null)
			{
				authModel.IsAuthenticated = false;
				authModel.Message = "Invalid token";
				return authModel;
			}

			RefreshToken refreshToken = user.RefreshTokens.Single(t => t.Token == token);

			if (!refreshToken.IsActive)
			{
				authModel.IsAuthenticated = false;
				authModel.Message = "Invalid Token";
				return authModel;
			}

			refreshToken.RevokedOn = DateTime.UtcNow;

			RefreshToken newRefreshToken = GenerateRefreshToken();
			user.RefreshTokens.Add(newRefreshToken);
			await _userManager.UpdateAsync(user);

			string jwtToken = await GenerateJwtToken(user);
			authModel.IsAuthenticated = true;
			authModel.Token = jwtToken;
			authModel.Email = user.Email;
			authModel.UserName = user.UserName;
			var roles = await _userManager.GetRolesAsync(user);
			authModel.Roles = roles.ToList();
			authModel.RefreshToken = newRefreshToken.Token;
			authModel.RefreshTokenExpiretion = newRefreshToken.ExpiresOn;

			return authModel;
		}
		public async Task<bool> RevokeTokenAsync(string token)
		{
			ApplicationUser user = _userManager.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
			if (user == null)
				return false;

			RefreshToken refreshToken = user.RefreshTokens.Single(t => t.Token == token);

			if(!refreshToken.IsActive)
				return false;

			refreshToken.RevokedOn = DateTime.UtcNow;
			await _userManager.UpdateAsync(user);
			return true;
		}
		public async Task<string> AddRoleAsync(string roleName)
		{
			IdentityRole role = await _roleManager.FindByNameAsync(roleName);
			if (role != null)
				return "Role is already exists";

			IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(roleName));

			if (result.Succeeded)
				return "Role saved in db successfully";

			return "Some problem happend while save role to db";
		}
	}
}
