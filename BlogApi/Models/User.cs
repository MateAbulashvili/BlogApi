using System;
using System.Collections.Generic;

namespace BlogApi.Models
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blog>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }
    }

    
}
