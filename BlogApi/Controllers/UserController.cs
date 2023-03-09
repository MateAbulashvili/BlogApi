using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
    }
}
