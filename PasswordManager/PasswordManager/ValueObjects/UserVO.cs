using PasswordManager.Model;
using System.Text.Json.Serialization;

namespace PasswordManager.ValueObjects
{
    public class UserVO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        [JsonIgnore]
        public string password { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
        public UserVO(User user, string token)
        {
            id = user.id;        
            name = user.name;
            username = user.username;
            Role = user.Role;
            Token = token;
        }
        public UserVO()
        {
        }
    }
}
