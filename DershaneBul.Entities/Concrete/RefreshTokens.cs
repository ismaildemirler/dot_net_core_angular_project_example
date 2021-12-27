using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DershaneBul.Entities.Abstract;

namespace DershaneBul.Entities.Concrete
{
    [Table("RefreshTokens")]
    public class RefreshTokens: IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Token { get; set; }
        public string JwtId { get; set; }  
        public DateTime CreationDate { get; set; } 
        public DateTime ExpiryDate { get; set; }  
        public bool Used { get; set; } 
        public bool Invalidated { get; set; } 
        public Guid UserId { get; set; }
    }
}
