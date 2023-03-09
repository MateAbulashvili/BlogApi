using AutoMapper;
using BlogApi.Models;

namespace BlogApi.Services
{
    public interface IBlogService
    {
        IEnumerable<Blog> GetAll();
        Blog GetById(int id);
        void Create(BlogRequestModel model);
        void Update(int id, Blog model);
        void Delete(int id);
    }

    public class BlogService : IBlogService
    {
        private readonly BlogDBContext _context;
        private readonly IMapper _mapper;
        public BlogService(BlogDBContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Create(BlogRequestModel model)
        {
            var blog = new Blog
            {
                UserId = model.UserId,
                Title = model.Title,
                Body = model.body,
                Summary = model.summary               
            };
            _context.Blogs.Add(blog);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var blog = _context.Blogs.Find(id);

            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Blog> GetAll()
        {
            return _context.Blogs.ToList();
        }

        public Blog GetById(int id)
        {
            return _context.Blogs.Find(id);
        }

        public void Update(int id, Blog model)
        {
            var blog = _context.Blogs.Find(id);

            if (blog != null)
            {
                blog.Title = model.Title;
                blog.Body = model.Body;
                blog.Summary = model.Summary;
                _context.SaveChanges();
            }
            
        }
    }

}
