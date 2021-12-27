using DershaneBul.Entities.Abstract;
using System.ComponentModel.DataAnnotations;

namespace DershaneBul.NGWebUI.Models
{
    public class LogoutViewModel: IDto
    {
        [Required(ErrorMessage = "Email gereklidir.")]
        public string Email { get; set; }
    }
}
