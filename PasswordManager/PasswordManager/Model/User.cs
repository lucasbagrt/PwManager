using PasswordManager.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Model
{
    [Table("users")]
    public class User : BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Column("username")]
        [Required]
        [StringLength(50)]
        public string username { get; set; }               

        [Column("password")]
        [DataType(DataType.Password)]
        [StringLength(150)]
        public string password { get; set; }
        public Role Role { get; set; }
    }
}
