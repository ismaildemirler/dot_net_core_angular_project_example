using DershaneBul.Business.Abstract.Users;
using DershaneBul.Core.DataAccess.Abstract;
using DershaneBul.DataAccess.Abstract;
using DershaneBul.Entities.Concrete;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DershaneBul.Business.Concrete.Users
{
    public class UserManager: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        IRepository<User> _repoUser = null;
        IQueryable<User> _repoUserQueryable = null;

        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _repoUser = _unitOfWork.GetRepository<User>();
            _repoUserQueryable = _repoUser.Queryable();
        }

        public async Task<BaseResponse> RegisterUserAsync(RequestUser request)
        {
            var user = new User
            {
                UserId = new Guid(),
                Email = request.Email,
                UserName = request.UserName,
                CreationDate = request.CreationDate,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                StateId = request.StateId, 
                UpdateDate = request.UpdateDate,
                UserTypeId = request.UserTypeId,
                PasswordHash = request.PasswordHash,
                PasswordSalt = request.PasswordSalt
            };

            _repoUser.Insert(user);
            await _unitOfWork.SaveAsync();
            return new BaseResponse
            {
                Success = true,
                Message = "Kullanıcı başarıyla kaydedilmiştir."
            };
        }

        public async Task<ResponseUser> GetUserAsync(RequestUser request)
        {
            var user = await _repoUserQueryable
                .FirstOrDefaultAsync(f => f.Email == request.Email);
            return new ResponseUser
            {
                User = user
            };
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            if (await _repoUserQueryable.AnyAsync(f => f.Email == email))
            {
                return true;
            }
            return false;
        }
    }
}
