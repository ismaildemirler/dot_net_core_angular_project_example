using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Time")]
    public class Time : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid TimeId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string TimeDescription { get; set; } 
    }
}
