using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MatchGoalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _configuration;
		private readonly IAuthRepository _authRepository;
		private readonly DateTime AccessTokenExpiresInMinutes;
		public AccountController(IAuthRepository authRepository,UserManager<ApplicationUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
			_authRepository = authRepository;
			AccessTokenExpiresInMinutes = DateTime.UtcNow.ToLocalTime().AddMinutes(_configuration.GetValue<int>("Jwt:ExpireTimeByMinutes"));
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
		{
			if (ModelState.IsValid)
			{
				AuthDto model = await _authRepository.RegisterAsync(registerUserDto);
				if (!model.IsAuthenticated)
					return BadRequest(model.Message);

				SetRefreshTokenInCookie(model.RefreshToken, model.RefreshTokenExpiretion);
				SetTokenInCookie(model.Token, AccessTokenExpiresInMinutes);

				return Ok(model);
			}
			return BadRequest(ModelState);
		}
		[HttpGet("refreshToken")]
		public async Task<IActionResult> RefreshToken()
		{
			string refreshToken = Request.Cookies["refreshToken"];
			AuthDto result = await _authRepository.RefreshTokenAsync(refreshToken);

			if (!result.IsAuthenticated)
				return BadRequest(result);

			SetRefreshTokenInCookie(result.RefreshToken,result.RefreshTokenExpiretion);

			return Ok(result);
		}
		[HttpPost("RevokeToken")]
		public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDto revokeTokenDto)
		{
			string revokeToken = revokeTokenDto.Token ?? Request.Cookies["refreshToken"];

			if (revokeToken == null)
				return BadRequest("Token is Invalid");

			bool result = await _authRepository.RevokeTokenAsync(revokeToken);

			if (!result)
				return BadRequest("Token is Invalid");

			return Ok();
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
		{ 
			if (!ModelState.IsValid) return BadRequest(ModelState);
			var result = await _authRepository.GetTokenAsync(loginUserDto);

			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			if (!string.IsNullOrEmpty(result.RefreshToken))
				SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiretion);

			SetTokenInCookie(result.Token, AccessTokenExpiresInMinutes);

			return Ok(result);
		}
		[HttpGet("AddRole")]
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> AddRole(string roleName)
		{
			return Ok(await _authRepository.AddRoleAsync(roleName));
		}
		[HttpPost("AddToRole")]
		[Authorize(AuthenticationSchemes = "Bearer,Cookies", Roles = "Admin")]
		public async Task<IActionResult> AddToRole([FromBody]AddToRoleDto addToRoleDto)
		{
			if (ModelState.IsValid)
			{
				string result = await _authRepository.AddToRoleAsync(addToRoleDto);
				return Ok(result);
			}
			return BadRequest(ModelState);
		}
		private void SetTokenInCookie(string Token,DateTime expires)
		{
			CookieOptions cookieOptions = new CookieOptions()
			{
				Expires = DateTime.Now.AddDays(7),
				HttpOnly = true,
				Secure = true,
				IsEssential = true,
				SameSite = SameSiteMode.None,
			};
			Response.Cookies.Append("accessToken", Token, cookieOptions);
		}
		private void SetRefreshTokenInCookie(string RefreshToken,DateTime expires)
		{
			CookieOptions cookieOptions = new CookieOptions()
			{
				Expires = expires.ToLocalTime(),
				HttpOnly = true,
				Secure = true,
				IsEssential = true,
				SameSite = SameSiteMode.None,
			};

			Response.Cookies.Append("refreshToken",RefreshToken,cookieOptions);
		}
	}
}
