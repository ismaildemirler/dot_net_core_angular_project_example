using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("City")]
    public class City : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CityId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string CityName { get; set; } 
    }
}
