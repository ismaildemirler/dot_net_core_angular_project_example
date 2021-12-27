using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("ProgramRegisterCalender")]
    public class ProgramRegisterCalender : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ProgramRegisterCalenderId { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        public string ProgramRegisterCalenderDescription { get; set; }
        [Required]
        public Guid FirmProgramId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string ProgramName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? TotalProgramTime { get; set; }
        public int? Quota { get; set; }
        public int? RemainingQuota { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
