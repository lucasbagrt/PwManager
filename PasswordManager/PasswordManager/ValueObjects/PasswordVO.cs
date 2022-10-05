using PasswordManager.Model;

namespace PasswordManager.ValueObjects
{
    public class PasswordVO
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }    
        public int app_id { get; set; }
        public int user_id { get; set; }
        public User User { get; set; }
        public Application Application { get; set; }
    }
}
