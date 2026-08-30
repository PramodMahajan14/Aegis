
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Aegis.DataAccess.Data;
using Aegis.Model.Auth;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Aegis.Utility.Common;

namespace Aegis.Services.Services
{
    public class RefreshTokenService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtSettings _jwtSettings;

        public RefreshTokenService(ApplicationDbContext context, IOptions<JwtSettings> options)
        {
            _context = context;
            _jwtSettings = options.Value;
        }


        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes).Replace("+", "_").Replace("/", "_").TrimEnd('=');
        }

        public string GenerateAccessToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };

            if (string.IsNullOrWhiteSpace(_jwtSettings.Key))
            {
                throw new Exception("JWT key is not configured.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: credential,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes)
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            return jwtTokenHandler.WriteToken(token);

        }



        public string GenerateAccessTokenWithTenanat(ApplicationUser user, Guid TenantId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("organization",TenantId.ToString()),

            };
            if (string.IsNullOrWhiteSpace(_jwtSettings.Key))
            {
                throw new Exception("JWT key is not configured.");
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: credential,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes)
            );

            var jwtTokenHandler = new JwtSecurityTokenHandler();

            return jwtTokenHandler.WriteToken(token);

        }

        public async Task SaveRefreshTokenAsync(string userId, string refreshToken)
        {
            var now = DateTime.UtcNow;

            var existingToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.UserId == userId && !x.IsRevoked);

            if (existingToken != null)
            {
                existingToken.Token = refreshToken;
                existingToken.CreatedAt = now;
                existingToken.ExpiresAt = now.AddDays(_jwtSettings.RefreshTokenExpiresInDays);
                existingToken.IsRevoked = false;
                existingToken.RevokedAt = null;
            }
            else
            {
                var token = new RefreshToken
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Token = refreshToken,
                    CreatedAt = now,
                    ExpiresAt = now.AddDays(_jwtSettings.RefreshTokenExpiresInDays),
                    IsRevoked = false
                };

                await _context.RefreshTokens.AddAsync(token);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> ValidateRefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return null;

            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(x =>
                    x.Token == refreshToken &&
                    !x.IsRevoked &&
                    x.ExpiresAt > DateTime.UtcNow);

            return token;
        }

        public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
        {
            ArgumentNullException.ThrowIfNull(refreshToken);

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<string> RotateRefreshTokenAsync(RefreshToken refreshToken)
        {
            ArgumentNullException.ThrowIfNull(refreshToken);

            // Revoke old token
            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            // Generate new token
            var newRefreshToken = GenerateRefreshToken();

            // Save new token
            var now = DateTime.UtcNow;

            var newToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = refreshToken.UserId,
                Token = newRefreshToken,
                CreatedAt = now,
                ExpiresAt = now.AddDays(_jwtSettings.RefreshTokenExpiresInDays),
                IsRevoked = false
            };

            await _context.RefreshTokens.AddAsync(newToken);
            await _context.SaveChangesAsync();

            return newRefreshToken;
        }


    }
}