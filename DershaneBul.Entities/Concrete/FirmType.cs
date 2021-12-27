using DershaneBul.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("FirmType")]
    public class FirmType : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int FirmTypeId { get; set; }

        [MaxLength(250)]
        [StringLength(250)]
        [Required]
        public string FirmTypeDescription { get; set; }
    }
}
