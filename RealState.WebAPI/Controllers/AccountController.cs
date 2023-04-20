using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.Account;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealState.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Manejo de sesion")]

    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateAsync(request));
        }

        [HttpPost("registerdeveloper")]
        public async Task<IActionResult> RegisterDeveloper(NewUserRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var origin = Request.Headers["origin"];

            RegisterRequest register = new();
            register.FirstName = request.FirstName;
            register.LastName = request.LastName;
            register.UserName = request.UserName;
            register.Email = request.Email;
            register.Phone = request.Phone;
            register.Password = request.Password;
            register.ConfirmPassword = request.ConfirmPassword;
            request.UserType = Roles.Developer.ToString();
            return Ok(await _accountService.RegisterBasicUserAsync(register, origin));
        }

        [HttpPost("registeradmin")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> RegisterAdmin(NewUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var origin = Request.Headers["origin"];

            RegisterRequest register = new();
            register.FirstName = request.FirstName;
            register.LastName = request.LastName;
            register.UserName = request.UserName;
            register.Email = request.Email;
            register.Phone = request.Phone;
            register.Password = request.Password;
            register.ConfirmPassword = request.ConfirmPassword;
            request.UserType = Roles.Admin.ToString();
            return Ok(await _accountService.RegisterBasicUserAsync(register, origin));
        }
    }
}
