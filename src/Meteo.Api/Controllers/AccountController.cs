using System.Threading.Tasks;
using Meteo.Core.Commands;
using Meteo.Core.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meteo.Api.Controllers
{
    [Route("")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody]SignUp command)
        {
            await _userService.SignUpAsync(command.Email, 
                command.Password, command.Role);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody]SignIn command)
            => Ok(await _userService.SignInAsync(command.Email, 
                    command.Password));


        [HttpGet("secure")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get()
            => Content($"Hello {User.Identity.Name}");        
    }
}