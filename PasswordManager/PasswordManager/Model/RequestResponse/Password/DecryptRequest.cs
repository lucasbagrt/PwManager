using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Model.RequestResponse.Password
{
    public class DecryptRequest
    {
        [Required]
        public string password { get; set; }
    }
}
