using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Elev8API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Elev8API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public AuthController(IConfiguration config, IAuthService authService)
        {
            _authService=authService;
            _configuration=config;
        }

        [HttpPost("login")]
        public IActionResult Login(string userName, string password)
        {
            var user = "john";
            var pass = "12345689";

            if (userName==user && password==pass)
            {
                return Ok(_authService.generateJWTToken());
            }
            else
            {
                return BadRequest("Invalid credentials");
            }
        }
    }
}
