using Iduff.Contracts;
using Iduff.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Iduff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAccountRepository accountRepository) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var response = await accountRepository.CreateAccount(userDto);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await accountRepository.LoginAccount(loginDto);
            return Ok(response);
        }
    }
}