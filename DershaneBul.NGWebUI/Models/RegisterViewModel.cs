using DershaneBul.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace DershaneBul.NGWebUI.Models
{
    public class RegisterViewModel: IDto
    {
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        [Required]
        public string PhoneNumber { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
