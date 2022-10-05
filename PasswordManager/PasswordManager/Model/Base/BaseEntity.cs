using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Model.Base
{
    public class BaseEntity
    {
        [Key]   
        public int id { get; set; }
    }
}
