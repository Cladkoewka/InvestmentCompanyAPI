using Application.DTOs.Auth;
using Application.Interfaces.Auth;
using Domain.Interfaces;
using Domain.Models.Auth;

namespace Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly PasswordHashingService _passwordHashingService;
        private readonly JwtTokenService _jwtTokenService;

        public AuthService(IAuthRepository authRepository, PasswordHashingService passwordHashingService, JwtTokenService jwtTokenService)
        {
            _authRepository = authRepository;
            _passwordHashingService = passwordHashingService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _authRepository.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User with such email already exists.");

            var passwordHash = _passwordHashingService.HashPassword(registerDto.Password);

            var newUser = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                RoleId = 1 
            };

            await _authRepository.CreateUserAsync(newUser);

            var token = _jwtTokenService.GenerateJwtToken(newUser);

            return token;
        }

        public async Task<string> LoginAsync(LoginDto loginDto)
        {
            var user = await _authRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
                throw new UnauthorizedAccessException("Incorrect email.");

            if (!_passwordHashingService.VerifyPassword(loginDto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Incorrect password.");

            var token = _jwtTokenService.GenerateJwtToken(user);

            return token;
        }
    }
}
