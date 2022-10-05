using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Model.RequestResponse.User;
public class CreateRequest
{
    public string name { get; set; }
    [Required]
    public string username { get; set; }
    [Required]
    public string password { get; set; }
    public Role Role { get; set; }
}