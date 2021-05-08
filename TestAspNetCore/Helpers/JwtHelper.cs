using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TestAspNetCore.Options;
using TestAspNetCore_Core.Entities;

namespace TestAspNetCore.Helpers
{
    public class JwtHelper
    {

        public static async Task<string> GenerateToken(User user, JwtSetting setting)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("data", user.Id.ToString()),
            };

            string result = string.Empty;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(setting.Issuer,
              setting.Audience,
              expires: DateTime.Now.AddHours(1),
              signingCredentials: creds,
              claims : claims);

            result = new JwtSecurityTokenHandler().WriteToken(token);
            return await Task.FromResult(result);
        }

        public static async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return await Task.FromResult(new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.UtcNow.AddDays(7),
                    Created = DateTime.UtcNow,
                    CreatedByIp = ipAddress
                });
            }
        }

        public static async Task<JwtSecurityToken> ValidateToken(JwtSetting _setting, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_setting.SecretKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _setting.Issuer,
                ValidAudience = _setting.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return await Task.FromResult(jwtToken);
        }
    }
}
