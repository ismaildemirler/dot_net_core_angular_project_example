using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("FeeRange")]
    public class FeeRange : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid FeeRangeId { get; set; }
        [MaxLength(500)]
        [StringLength(500)]
        [Required]
        public string FeeRangeDescription { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
