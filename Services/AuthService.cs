using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Elev8API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration=configuration;
        }
        public string generateJWTToken()
        {
            try
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, "serhat"));
                var keyValue = _configuration.GetSection("JwtSetting:Key").Value;
                var key = Encoding.UTF8.GetBytes(keyValue);

                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                );

                var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

                return token;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
