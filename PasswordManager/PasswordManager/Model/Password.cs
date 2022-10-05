using PasswordManager.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Model
{
    [Table("passwords")]
    public class Password : BaseEntity
    {
        [Column("username")]
        [Required]
        [StringLength(100)]
        public string username { get; set; }

        [Column("password")]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string password { get; set; }

        [ForeignKey("user_id")]
        public virtual User User { get; set; }

        [ForeignKey("application_id")]
        public virtual Application Application { get; set; }
    }
}
