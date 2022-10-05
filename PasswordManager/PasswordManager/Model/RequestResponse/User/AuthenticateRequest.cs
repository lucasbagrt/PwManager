using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Model.RequestResponse.User;
public class AuthenticateRequest
{
    [Required]
    public string username { get; set; }

    [Required]
    public string password { get; set; }
}