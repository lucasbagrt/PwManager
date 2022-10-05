using PasswordManager.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PasswordManager.Model
{
    [Table("applications")]
    public class Application : BaseEntity
    {
        [Column("name")]
        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Column("icon")]        
        [StringLength(50)]
        public string icon { get; set; }        
    }
}
