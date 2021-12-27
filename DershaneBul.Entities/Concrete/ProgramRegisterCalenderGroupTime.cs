using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("ProgramRegisterCalenderGroupTime")]
    public class ProgramRegisterCalenderGroupTime : IBaseEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ProgramRegisterCalenderGroupTimeId { get; set; }
        [Required]
        public Guid ProgramRegisterCalenderId { get; set; }
        [Required]
        public Guid GroupTimeId { get; set; } 
    }
}
