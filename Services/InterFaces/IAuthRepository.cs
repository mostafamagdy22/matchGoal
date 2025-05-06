namespace MatchGoalAPI.Services.InterFaces
{
	/// <summary>
	/// Defines the interface for authentication-related operations, including user registration, login, role management, and token handling.
	/// </summary>
	public interface IAuthRepository
	{
		/// <summary>
		/// Authenticates a user based on the provided login credentials and returns an authentication token if successful.
		/// </summary>
		/// <param name="loginUserDto">The data transfer object containing the user's email and password for login.</param>
		/// <returns>An <see cref="AuthDto"/> object containing the authentication result, including a JWT token if successful, or an error message if authentication fails.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="loginUserDto"/> is null.</exception>
		public Task<AuthDto> GetTokenAsync(LoginUserDto loginUserDto);
		/// <summary>
		/// Registers a new user with the provided registration details and assigns a default role and authentication token.
		/// </summary>
		/// <param name="registerUserDto">The data transfer object containing the user's registration information (username, email, and password).</param>
		/// <returns>An <see cref="AuthDto"/> object containing the authentication result, including a JWT token if registration succeeds, or an error message if it fails.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="registerUserDto"/> is null.</exception>
		/// <exception cref="InvalidOperationException">Thrown if the default role cannot be assigned due to a missing role.</exception>
		public Task<AuthDto> RegisterAsync(RegisterUserDto registerUserDto);
		/// <summary>
		/// Assigns a specified role to a user if the user and role exist and the user does not already have the role.
		/// </summary>
		/// <param name="addRoleModel">The data transfer object containing the user ID and the role to be assigned.</param>
		/// <returns>A <see cref="string"/> indicating the result of the operation (e.g., "Role added successfully" or an error message).</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="addRoleModel"/> is null.</exception>
		/// <exception cref="InvalidOperationException">Thrown if the user or role does not exist.</exception>
		public Task<string> AddToRoleAsync(AddToRoleDto addRoleModel);
		/// <summary>
		/// Refreshes the user's JWT token using a valid refresh token if the user is authenticated and has no active refresh token.
		/// </summary>
		/// <param name="token">The refresh token used to generate a new JWT token.</param>
		/// <returns>An <see cref="AuthDto"/> object containing the new authentication result, including a refreshed JWT token if successful, or an error message if it fails.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="token"/> is null or empty.</exception>
		/// <exception cref="InvalidOperationException">Thrown if the refresh token is invalid or expired.</exception>
		public Task<AuthDto> RefreshTokenAsync(string token);
		/// <summary>
		/// Revokes a specified refresh token, preventing its use for future token refresh requests.
		/// </summary>
		/// <param name="token">The refresh token to revoke.</param>
		/// <returns>A <see cref="bool"/> indicating whether the token was successfully revoked.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="token"/> is null or empty.</exception>
		/// <exception cref="InvalidOperationException">Thrown if the token is not found or already revoked.</exception>
		public Task<bool> RevokeTokenAsync(string token);
		public Task<string> AddRoleAsync(string roleName);
	}
}
