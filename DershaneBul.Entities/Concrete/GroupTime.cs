using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("GroupTime")]
    public class GroupTime : IBaseEntity 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid GroupTimeId { get; set; }
        [Required]
        public Guid TimeId { get; set; }
        [Required]
        public Guid GroupId { get; set; } 
    }
}
