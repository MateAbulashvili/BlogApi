using BlogApi.Models;
using BlogApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogDBContext _context;
        private readonly IBlogService _blogService;
        public BlogController(BlogDBContext context, IBlogService blogService)
        {
            _context = context;
            _blogService = blogService;
        }
        [HttpGet("GetAll")]
        
        IActionResult GetAll()
        {
            var users = _blogService.GetAll();
            return Ok(users);
        }

        [HttpGet("GetByID")]
        public IActionResult GetById(int id)
        {
            var user = _blogService.GetById(id);
            return Ok(user);
        }

        [HttpPost("CreateBlog")]
        public IActionResult Create(BlogRequestModel model)
        {
            _blogService.Create(model);
            return Ok(new { message = "Blog created" });
        }
        [HttpPut("UpdateBlog")]
        public IActionResult Update(int id, Blog model)
        {

            _blogService.Update(id, model);
            return Ok(new { message = "Blog updated" });
        }
        [HttpDelete("DeleteBlog")]
        public IActionResult Delete(int id)
        {
            _blogService.Delete(id);
            return Ok(new { message = "Blog deleted" });
        }
    }

}
