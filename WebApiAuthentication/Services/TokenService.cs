using CommonUtilities.CommonVariables;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApiAuthentication.Services
{
    public class TokenService : ITokenService
    {
        private TimeSpan ExpiryDuration = new TimeSpan(20, 30, 0);

        /// <summary>
        /// Generates the bearer token for authentication
        /// </summary>
        /// <param name="key">The unique key</param>
        /// <param name="issuer">The issuer</param>
        /// <param name="audience">The audience collection</param>
        /// <param name="userName">The unique username</param>
        /// <returns>Bearer token holding specified key and username</returns>
        public string BuildToken(string key, string issuer, IEnumerable<string> audience, string userName)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                new Claim("Role", Common.userRole)
            };

                claims.AddRange(audience.Select(aud => new Claim(JwtRegisteredClaimNames.Aud, aud)));

                var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims,
                    expires: DateTime.Now.Add(ExpiryDuration), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            }
            catch (Exception ex)
            {
                return Common.tokenError;
            }            
        }
    }
}
