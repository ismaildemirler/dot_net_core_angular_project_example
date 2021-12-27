using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("FirmProgram")]
    public class FirmProgram : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid FirmProgramId { get; set; }
        [Required]
        public Guid FeeRangeId { get; set; }
        [Required]
        public Guid ProgramId { get; set; }
        [Required]
        public Guid FirmId { get; set; }
        [Required] 
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
