using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DershaneBul.Entities.Abstract;

namespace DershaneBul.Entities.Concrete
{
    [Table("User")]
    public class User: IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid UserId { get; set; }
        [MaxLength(70)]
        [StringLength(70)]
        [Required]
        public string FullName { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string UserName { get; set; }
   
        [MaxLength(50)]
        [StringLength(50)]
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int StateId { get; set; }

        [Required]
        public int UserTypeId { get; set; } 
        public byte[] PasswordSalt { get; set; }

        public byte[] PasswordHash { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; } 
        
    }
}
