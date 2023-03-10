using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BlogDBContext _context;
        private readonly IUserService _userService;
        public UserController(BlogDBContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
       
        [HttpGet]
        IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("Get")]
        public IActionResult GetById(int id)
        {
            var user = _userService.GetById(id);
            return Ok(user);
        }

        [HttpPost("CreateUser")]
        public IActionResult Create(CreateUserRequestModel model)
        {
            _userService.Create(model);
            return Ok(new { message = "User created" });
        }
        [HttpPut("Update")]
        public IActionResult Update(int id, User model)
        {

            _userService.Update(id, model);
            return Ok(new { message = "User updated" });
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok(new { message = "User deleted" });
        }
        [HttpGet("Admins")]
        [Authorize(Roles = "Admin")] 
        public IActionResult AdminsEndpoint()
        {
            var currentUser = GetCurrentUser();
            return Ok($"Hi you are authorized");
        }
        private User GetCurrentUser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;
                return new User
                {
                    FirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    LastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                };
                
            }
            return null;
        }
    }
}
