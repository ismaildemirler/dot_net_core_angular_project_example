using DershaneBul.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace DershaneBul.NGWebUI.Models
{
    public class LoginViewModel: IDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
