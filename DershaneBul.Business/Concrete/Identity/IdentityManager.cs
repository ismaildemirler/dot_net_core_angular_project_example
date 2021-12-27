using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DershaneBul.Business.Abstract.Identity;
using DershaneBul.Business.Abstract.Users;
using DershaneBul.Core.DataAccess.Abstract;
using DershaneBul.Core.Models;
using DershaneBul.Core.Utilities.Security;
using DershaneBul.DataAccess.Abstract;
using DershaneBul.Entities.Concrete;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DershaneBul.Business.Concrete.Identity
{
    public class IdentityManager : IIdentityService
    {
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        IRepository<User> _repoIdentity = null;
        IQueryable<User> _repoIdentityQueryable = null;
        IRepository<RefreshTokens> _repoRefreshToken = null;
        IQueryable<RefreshTokens> _repoRefreshTokenQueryable = null;

        public IdentityManager(
            TokenValidationParameters tokenValidationParameters,
            JwtSettings jwtSettings,
            IUserService userService,
            IUnitOfWork unitOfWork)
        {
            _tokenValidationParameters = tokenValidationParameters;
            _jwtSettings = jwtSettings;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _repoIdentity = _unitOfWork.GetRepository<User>();
            _repoIdentityQueryable = _repoIdentity.Queryable();
            _repoRefreshToken = _unitOfWork.GetRepository<RefreshTokens>();
            _repoRefreshTokenQueryable = _repoRefreshToken.Queryable();
        }

        public async Task<ResponseAuthentication> GenerateRefreshTokenAsync(string token, string refreshToken)
        {
            var validatedToken = GetPrincipalFromToken(token);
            if (validatedToken == null)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { "Anahtarınız geçersiz bir anahtardır!" }
                };
            }

            var expiryDateUnix = long.Parse(validatedToken.Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTime > DateTime.Now)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { "Bu anahtarın kullanım süresi henüz bitmemiştir!" }
                };
            }

            var jti = validatedToken.Claims
                .Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var guidToken = new Guid(refreshToken);
            var storedRefreshToken = await _repoRefreshTokenQueryable
                .SingleOrDefaultAsync(x => x.Token == guidToken);

            if (storedRefreshToken == null)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { "Böyle bir anahtar bulunamamıştır!" }
                };
            }

            if (DateTime.Now > storedRefreshToken.ExpiryDate)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { "Bu anahtarın kullanım süresi dolmuştur!" }
                };
            }

            if (storedRefreshToken.Invalidated)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { "Bu anahtar geçersizdir!" }
                };
            }

            if (storedRefreshToken.Used)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { "Bu anahtar daha önce kullanılmıştır!" }
                };
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] {
                        "Bu anahtar kullanmakta olduğunuz anahtarla uyuşmamaktadır!"
                    }
                };
            }

            storedRefreshToken.Used = true;
            _repoRefreshToken.Update(storedRefreshToken);
            await _unitOfWork.SaveAsync();

            var id = validatedToken.Claims.Single(x => x.Type == "id").Value;
            var userId = new Guid(id);
            var user = await _repoIdentityQueryable
                .FirstOrDefaultAsync(f => f.UserId == userId);
            return await CreateJwtTokenAsync(user);
        }

        public async Task<ResponseRefreshToken> GetExistingRefreshTokenAsync(string email)
        {
            var userResponse = await _userService.GetUserAsync(
                new RequestUser
                {
                    Email = email
                });

            var user = userResponse.User;
            RefreshTokens existingToken = null;
            if (user != null)
            {
                existingToken = await _repoRefreshTokenQueryable
                    .FirstOrDefaultAsync(f => f.Used == false && f.UserId == user.UserId);
            }
            return new ResponseRefreshToken
            {
                RefreshToken = existingToken
            };
        }

        public async Task<ResponseAuthentication> LoginAsync(string email, string password)
        {
            var user = await _repoIdentityQueryable
                .FirstOrDefaultAsync(f => f.Email == email);
            if (user == null)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { @"Böyle bir kullanıcı bulunamamıştır. 
                        Lütfen kayıt olunuz!" }
                };
            }

            if (user.StateId != 1)
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { @"Üyeliğiniz yöneticilerimiz tarafından verilecek
                        onayı beklemektedir." }
                };
            }

            if (!CryptoHelper.VerifyPasswordHash(
                    password, user.PasswordHash, user.PasswordSalt))
            {
                return new ResponseAuthentication
                {
                    Message = "Uyarı",
                    Details = new[] { @"Böyle bir kullanıcı bulunamamıştır. 
                        Lütfen kayıt olunuz!" }
                };
            }

            var token = await CreateJwtTokenAsync(user);

            return new ResponseAuthentication
            {
                Message = "Başarıyla giriş yaptınız.",
                Success = true,
                Token = token.Token,
                RefreshToken = token.RefreshToken,
                Details = token.Details
            };
        }

        public async Task<ResponseAuthentication> CreateJwtTokenAsync(User user)
        {
            var now = DateTime.Now;
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _jwtSettings.Secret;
            var key = Encoding.ASCII.GetBytes(secret);
            var expires = now.Add(_jwtSettings.TokenLifetime);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("id", user.UserId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Sub, user.FullName),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                IssuedAt = now.AddHours(3),
                Expires = expires.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            var detail = token.ValidTo.ToShortDateString() + " "
                + token.ValidTo.ToShortTimeString()
                + " geçerlilik zamanıdır.";

            var refreshToken = new RefreshTokens
            {
                Token = Guid.NewGuid(),
                JwtId = token.Id,
                UserId = user.UserId,
                CreationDate = DateTime.Now.AddHours(3),
                ExpiryDate = DateTime.Now.AddMonths(6)
            };

            _repoRefreshToken.Insert(refreshToken);
            await _unitOfWork.SaveAsync();

            return new ResponseAuthentication
            {
                Success = true,
                Token = tokenString,
                RefreshToken = refreshToken.Token.ToString(),
                Details = new[] { detail }
            };
        }

        public async Task<BaseResponse> LogOutAsync(string email)
        {
            var user = await _repoIdentityQueryable
                .FirstOrDefaultAsync(f => f.Email == email);
            if (user != null)
            {
                var tokens = await _repoRefreshTokenQueryable.Where(
                    f => f.UserId == user.UserId).ToListAsync();

                _repoRefreshToken.DeleteRange(tokens);
                await _unitOfWork.SaveAsync();
            }
            return new BaseResponse
            {
                Success = true,
            };
        }

        public async Task<BaseResponse> RegisterAsync(RequestUser request)
        {
            var userExists = await _userService.UserExistsAsync(request.Email);
            if (userExists)
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Uyarı",
                    Details = new[] { "Bu kullanıcı sistemimizde zaten kayıtlıdır!" }
                };
            }

            byte[] passwordHash, passwordSalt;
            CryptoHelper.CreatePasswordHash(request.Password,
                out passwordHash, out passwordSalt);

            request.PasswordHash = passwordHash;
            request.PasswordSalt = passwordSalt;
            request.CreationDate = DateTime.Now;
            request.UpdateDate = DateTime.Now;
            request.UserTypeId = 0;
            request.StateId = 0;

            await _userService.RegisterUserAsync(request);
            return new BaseResponse
            {
                Success = true,
                Message = "Kayıt işleminiz başarıyla gerçekleşmiştir."
            };
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(
                token, _tokenValidationParameters, out var validatedToken);
            if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
            {
                return null;
            }

            return principal;
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            var i = validatedToken is JwtSecurityToken;

            JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)validatedToken;
            var j = jwtSecurityToken.Header.Alg.Equals(
                       SecurityAlgorithms.HmacSha512Signature,
                       StringComparison.InvariantCultureIgnoreCase);

            return (i && j);
        }
    }
}
