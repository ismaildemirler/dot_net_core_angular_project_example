using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;
using System.Threading.Tasks;

namespace DershaneBul.Business.Abstract.Identity
{
    public interface IIdentityService
    {
        Task<BaseResponse> RegisterAsync(RequestUser request);
        Task<ResponseRefreshToken> GetExistingRefreshTokenAsync(string email);
        Task<BaseResponse> LogOutAsync(string email);

        Task<ResponseAuthentication> LoginAsync(string email, string password);

        Task<ResponseAuthentication> GenerateRefreshTokenAsync(
            string token, string refreshToken);
    }
}
