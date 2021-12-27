using AutoMapper;
using DershaneBul.NGWebUI.Models;
using DershaneBul.Business.Abstract.Identity;
using DershaneBul.Business.Abstract.Users;
using DershaneBul.Core.NetCore.ActionFilters;
using DershaneBul.Entities.Containers.Request;
using DershaneBul.Entities.Containers.Response;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DershaneBul.NGWebUI.Controllers
{
    [Produces("application/json")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public IdentityController(
            IIdentityService identityService,
            IUserService userService,
            IMapper mapper
            )
        {
            _identityService = identityService;
            _userService = userService;
            _mapper = mapper;
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(
            [FromBody] RegisterViewModel registerViewModel)
        {
            var userToCreate = _mapper.Map<RequestUser>(registerViewModel);
            var registerResponse = await _identityService
                .RegisterAsync(userToCreate);

            if (!registerResponse.Success)
            {
                return BadRequest(registerResponse);
            }
            return Ok(registerResponse);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(
            [FromBody] LoginViewModel loginViewModel)
        {
            //int a = 0;
            //int b = 5 / a;

            var refreshTokenResponse = await _identityService
                .GetExistingRefreshTokenAsync(loginViewModel.Email);
            var refreshToken = refreshTokenResponse.RefreshToken;
            if (refreshToken == null)
            {
                var loginResponse = await _identityService.LoginAsync(
                                loginViewModel.Email, loginViewModel.Password);
                if (!loginResponse.Success)
                {
                    return BadRequest(loginResponse);
                }
                return Ok(loginResponse);
            }
            return Ok(new ResponseAuthentication
            {
                Success = true
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenAsync(
            [FromBody] RefreshTokenViewModel refreshTokenViewModel)
        {
            var authResponse = await _identityService
                .GenerateRefreshTokenAsync(refreshTokenViewModel.Token,
                    refreshTokenViewModel.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOutAsync(
            [FromBody] LogoutViewModel logoutViewModel)
        {
            var authResponse = await _identityService.LogOutAsync(logoutViewModel.Email);

            if (!authResponse.Success)
            {
                return BadRequest(authResponse);
            }

            return Ok(authResponse);
        }
    }
}