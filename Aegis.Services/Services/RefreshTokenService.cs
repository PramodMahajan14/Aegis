
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
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };

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



        public string GenerateAccessTokenWithTenanat(ApplicationUser user,Guid TenantId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("organization",TenantId.ToString()),

            };

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
            var token = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = refreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiresInDays),
                IsRevoked = false
            };
             
             var existing = _context.RefreshTokens.Any(x=>x.Id == GuidUtility.ToGuid(userId));
            if (existing)
            {
                _context.RefreshTokens.Update(token);
            }
            else
            {
                _context.RefreshTokens.Add(token);
            }
             

             await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> ValidateRefreshTokenAsync(string refreshToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
                return null;

            var token = await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (token == null)
                return null;

            if (token.IsRevoked)
                return null;

            if (token.ExpiresAt <= DateTime.UtcNow)
                return null;

            return token;
        }

        public async Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                throw new ArgumentNullException(nameof(refreshToken));

            refreshToken.IsRevoked = true;
            refreshToken.RevokedAt = DateTime.UtcNow;

            _context.RefreshTokens.Update(refreshToken);

            await _context.SaveChangesAsync();
        }

        public async Task<string> RotateRefreshTokenAsync(RefreshToken refreshToken)
        {
            if (refreshToken == null)
                throw new ArgumentNullException(nameof(refreshToken));

            // Revoke old refresh token
            await RevokeRefreshTokenAsync(refreshToken);

            // Generate new refresh token
            var newRefreshToken = GenerateRefreshToken();

            // Save new refresh token
            await SaveRefreshTokenAsync(refreshToken.User, newRefreshToken);

            // Return new token to client
            return newRefreshToken;
        }
    }
}