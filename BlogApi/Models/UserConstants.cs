namespace BlogApi.Models
{
    public class UserConstants
    {
        public static List<User> Users = new List<User>()
        {
            new User() { FirstName = "Admin",Email = "admin@gmail.com",Password = "admin123",LastName = "lastname"}
        };
        
    }
}
