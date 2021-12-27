using DershaneBul.Entities.Abstract;

namespace DershaneBul.NGWebUI.Models
{
    public class RefreshTokenViewModel : IDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
