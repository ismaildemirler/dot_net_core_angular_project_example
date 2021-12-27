using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Group")]
    public class Group : IBaseEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid GroupId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        public string GroupDescription { get; set; } 
    }
}
