using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Property")]
    public class Property : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid PropertyId { get; set; }
        [MaxLength(150)]
        [StringLength(150)]
        [Required]
        public string PropertyDescription { get; set; } 
        public Guid? FilterTypeId { get; set; }
        [Required]
        public bool IsUseInFilter { get; set; }
        [MaxLength(50)]
        [StringLength(50)] 
        public string Icon { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public int StateId { get; set; } 
        public Guid? PropertyGroupId { get; set; }
    }
}
