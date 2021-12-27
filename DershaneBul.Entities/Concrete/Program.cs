using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Program")]
    public class Program : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ProgramId { get; set; }
        [MaxLength(500)]
        [StringLength(500)]
        [Required]
        public string ProgramDescription { get; set; }
        [Required]
        public int IsActive { get; set; }
        [Required] 
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
