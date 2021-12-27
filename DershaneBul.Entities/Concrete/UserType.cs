using DershaneBul.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("UserType")]
    public class UserType : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int UserTypeId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string UserTypeDescription { get; set; } 
    }
}
