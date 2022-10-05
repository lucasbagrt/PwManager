using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Model.RequestResponse.Password
{
    public class SaveRequest
    {
        [Required]
        public int app_id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }        
    }
}
