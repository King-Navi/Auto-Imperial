namespace AutoImperialDAO.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int Id { get; set; }

        public User() { }
        public User(string username, string password, string role, int id)
        {
            Username = username;
            Password = password;
            Role = role;
            Id = id;
        }
    }
}
