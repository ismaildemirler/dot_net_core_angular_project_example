using DershaneBul.Entities.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Town")]
    public class Town : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TownId { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string TownName { get; set; }
        [Required]
        public int CityId { get; set; } 
    }
}
