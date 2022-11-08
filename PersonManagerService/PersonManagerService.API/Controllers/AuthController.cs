using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PersonManagerService.Application.DTOs;
using PersonManagerService.Domain.Commands.CreatePerson;
using PersonManagerService.Domain.DTOs;
using PersonManagerService.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Net.WebRequestMethods;

namespace PersonManagerService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static User user = new User();
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            user.Username = request.Username;
            var hashAndSalt = CreatePasswordHashAndSalt(request.Password);
            user.PasswordHash = hashAndSalt.Item1;
            user.PasswordSalt = hashAndSalt.Item2;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserDto request)
        {
            if (request.Username == user.Username && VerifyPasswordHash(request.Password))
            {
                string token = CreateToken(user);
                return Ok(token);
            }
            return Unauthorized();
        }

        private string CreateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("SecurityKey").Value));
            var signingCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:7080",
                audience: "https://localhost:7080",
                claims: new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "admin")
                },
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signingCreds);


            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private (byte[], byte[]) CreatePasswordHashAndSalt(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return new(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), hmac.Key);
            }
        }

        private bool VerifyPasswordHash(string password)
        {
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(user.PasswordHash);
            }
        }
    }
}
