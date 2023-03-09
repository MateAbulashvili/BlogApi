namespace BlogApi.Models
{
    public class BlogRequestModel
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string body { get; set; }
        public string summary { get; set; }
    }
}
