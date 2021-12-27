using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("FirmProperty")]
    public class FirmProperty : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid FirmProgramPropertyId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string FirmProgramPropertyValue { get; set; }
        [Required]
        public Guid RelatedId { get; set; }
        [Required]
        public Guid PropertyId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public int RelatedTypeId { get; set; }
        [Required] 
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
 
    }
}
