using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReadingIsGood.Domain.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReadingIsGood.API
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly ILogger<TokenGenerator> _logger;
        private readonly JwtSettings _jwtSettings;

        public TokenGenerator(ILogger<TokenGenerator> logger,
            IOptions<JwtSettings> jwtSettings)
        {
            _logger = logger;
            _jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken(int userId)
        {
            try
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(claims: claims,
                                expires: DateTime.Now.AddDays(_jwtSettings.ExpireDay),
                                signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return string.Empty;
            }
        }
    }
}
