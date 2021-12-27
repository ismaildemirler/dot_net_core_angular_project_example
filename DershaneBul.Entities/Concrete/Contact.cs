using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Contact")]
    public class Contact : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ContactId { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        [Required]
        public string ContactDescription { get; set; }
        [Required]
        public Guid FirmId { get; set; }
        [Required]
        public int ContactTypeId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required] 
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
