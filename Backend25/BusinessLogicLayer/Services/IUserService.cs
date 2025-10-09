using BusinessLogicLayer.Configuration;
using BusinessLogicLayer.DTOs;
using Core.Models;
using DataAccess.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetProfileAsync(Guid userId);
        Task<UserProfileDto> UpdateProfileAsync(Guid userId, UserProfileDto profileDto);
        Task<TokenResponseDto> LoginAsync(LoginDto loginDto);
        Task<TokenResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto);
        Task<bool> LogoutAsync(Guid userId);
    }

    public class UserService : IUserService
    {
        private readonly ElsheenaDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly JwtBearerTokenSettings _jwtSettings;

        public UserService(
            ElsheenaDbContext context,
            UserManager<User> userManager,
            IOptions<JwtBearerTokenSettings> jwtSettings)
        {
            _context = context;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<UserProfileDto> GetProfileAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id = {userId} not found!");
            }

            return new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber ?? ""
            };
        }

        public async Task<UserProfileDto> UpdateProfileAsync(Guid userId, UserProfileDto profileDto)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User with id = {userId} not found!");
            }

            user.FullName = profileDto.FullName;
            user.BirthDate = profileDto.BirthDate;
            user.Gender = profileDto.Gender;
            user.Address = profileDto.Address;
            user.PhoneNumber = profileDto.PhoneNumber;
            user.ModifyDateTime = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("Failed to update user profile");
            }

            return profileDto;
        }

        public async Task<TokenResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var token = await GenerateJwtTokenAsync(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshExpiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryTimeInDays);
            await _userManager.UpdateAsync(user);

            return new TokenResponseDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtSettings.ExpiryTimeInMinutes * 60
            };
        }

        public async Task<TokenResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                BirthDate = registerDto.BirthDate,
                Gender = registerDto.Gender,
                Address = registerDto.Address,
                PhoneNumber = registerDto.PhoneNumber,
                CreateDateTime = DateTime.UtcNow,
                ModifyDateTime = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            var token = await GenerateJwtTokenAsync(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshExpiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryTimeInDays);
            await _userManager.UpdateAsync(user);

            return new TokenResponseDto
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                ExpiresIn = _jwtSettings.ExpiryTimeInMinutes * 60
            };
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshTokenDto.RefreshToken);

            if (user == null || user.RefreshExpiration <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            var token = await GenerateJwtTokenAsync(user);
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshExpiration = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryTimeInDays);
            await _userManager.UpdateAsync(user);

            return new TokenResponseDto
            {
                AccessToken = token,
                RefreshToken = newRefreshToken,
                ExpiresIn = _jwtSettings.ExpiryTimeInMinutes * 60
            };
        }

        public async Task<bool> LogoutAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return false;
            }

            user.RefreshToken = null;
            user.RefreshExpiration = DateTime.UtcNow.AddDays(-1);
            await _userManager.UpdateAsync(user);

            return true;
        }

        private async Task<string> GenerateJwtTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryTimeInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }

    public class LoginDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class RegisterDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }

    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public int ExpiresIn { get; set; }
    }

    public class RefreshTokenDto
    {
        public string RefreshToken { get; set; } = null!;
    }
}