using DershaneBul.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("RelatedType")]
    public class RelatedType : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int RelatedTypeeId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string RelatedTypeDescription { get; set; } 
    }
}
