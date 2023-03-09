using System;
using System.Collections.Generic;

namespace BlogApi.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public string? Summary { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
