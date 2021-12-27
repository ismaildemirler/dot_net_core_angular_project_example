using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Firm")]
    public class Firm : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid FirmId { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        [Required]
        public string FirmName { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [MaxLength(500)]
        [StringLength(500)]
        [Required]
        public string FirmDescription { get; set; }
        [Required]
        public Guid AddressId { get; set; }
        public int FirmType { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
