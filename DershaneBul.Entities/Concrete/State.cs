using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DershaneBul.Entities.Abstract;

namespace DershaneBul.Entities.Concrete
{
    [Table("State")]
    public class State : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StateId { get; set; }
        public string StateDescription { get; set; } 
    }
}
