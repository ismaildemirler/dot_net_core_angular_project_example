using DershaneBul.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DershaneBul.Entities.Concrete
{
    [Table("Comment")]
    public class Comment : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid CommentId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid FirmId { get; set; }
        [MaxLength(500)]
        [StringLength(500)]
        [Required]
        public string CommentDescription { get; set; }
        [Required]
        public float Point { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
    }
}
