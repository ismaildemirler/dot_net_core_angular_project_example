using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Media")]
    public class Media : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid MediaId { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        [Required]
        public string MediaDescription { get; set; }
        [MaxLength]
        [Required]
        public byte[] BlobData { get; set; }
        [Required]
        public int Sort { get; set; }
        [Required]
        public int MediaTypeId { get; set; }
        [Required]
        public Guid FirmId { get; set; }
        [Required]
        public int StateId { get; set; } 
        [Required] 
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
