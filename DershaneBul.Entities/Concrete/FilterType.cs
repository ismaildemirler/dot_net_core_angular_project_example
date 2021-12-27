using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DershaneBul.Entities.Abstract;

namespace DershaneBul.Entities.Concrete
{
    [Table("FilterType")]
    public class FilterType : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid FilterTypeId { get; set; }
        [MaxLength(100)]
        [StringLength(100)]
        public string FilterTypeDescription { get; set; }
    }
}
