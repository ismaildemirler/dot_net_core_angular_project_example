using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("ContactType")]
    public class ContactType : IBaseEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int ContactTypeId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string ContactTypeDescription { get; set; }

        [MaxLength(50)]
        [StringLength(50)] 
        public string Icon { get; set; }
    }
}
