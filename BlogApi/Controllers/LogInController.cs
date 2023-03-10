using BlogApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private IConfiguration _config;
        private BlogDBContext _context;
        public LogInController(IConfiguration config, BlogDBContext context)
        {
            _config = config;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult LogIn([FromBody] UserLogIn login)
        {
            var user = Authenticate(login);
            if (user == null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("user not found");

        }

        private string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new [] 
            {
                new Claim(ClaimTypes.NameIdentifier,user.FirstName),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Surname,user.LastName)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials : credentials  
                );
                
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private User Authenticate(UserLogIn user)
        {
            var currentUser = _context.Users.FirstOrDefault(x => x.Email.ToLower() == user.Email.ToLower() && x.Password == user.Password);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }

    }
}
