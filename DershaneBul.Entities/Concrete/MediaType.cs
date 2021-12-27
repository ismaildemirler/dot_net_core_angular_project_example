using DershaneBul.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("MediaType")]
    public class MediaType : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int MediaTypeId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string MediaTypeDescription { get; set; } 
    }
}
