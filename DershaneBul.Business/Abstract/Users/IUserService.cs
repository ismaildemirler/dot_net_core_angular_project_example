using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;
using System.Threading.Tasks;

namespace DershaneBul.Business.Abstract.Users
{
    public interface IUserService
    {
        Task<BaseResponse> RegisterUserAsync(RequestUser user);
        Task<bool> UserExistsAsync(string email);
        Task<ResponseUser> GetUserAsync(RequestUser request);
    }
}
