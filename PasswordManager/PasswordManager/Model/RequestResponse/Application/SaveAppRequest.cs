using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Model.RequestResponse.Application
{
    public class SaveAppRequest
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string icon { get; set; }
    }
}
