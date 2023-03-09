using AutoMapper;
using BlogApi.Models;

namespace BlogApi.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        void Create(CreateUserRequestModel model);
        void Update(int id, User model);
        void Delete(int id);
    }
    public class UserService : IUserService
    {
        private readonly BlogDBContext _context;
        private readonly IMapper _mapper;
        public UserService(BlogDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Create(CreateUserRequestModel model)
        {
            if (_context.Users.Any(x => x.Email == model.Email))
            {
                throw new Exception($"user with this email{model.Email} already exists");
            }
            User user = new User();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password = model.Password;
            //var user = _mapper.Map<User>(model);
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = GetUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(int id)
        {
            return GetUser(id);
        }

        public void Update(int id, User model)
        {
            var user = GetUser(id);
            if (model.Email != user.Email && _context.Users.Any(x => x.Email == model.Email))
                throw new Exception("User with the email '" + model.Email + "' already exists");
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();

        }

        // helper method
        private User GetUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new NullReferenceException(" NO user with this Id ");
            }
            return user;
        }
    }
}
