using InventryOrderManagementSystem.BLL.DTOs;
using InventryOrderManagementSystem.BLL.SeviceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventryOrderManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices, ILogger<AuthController> logger) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            logger.LogInformation($"Login Attempt By User {loginDto.Email}");
            var result = await authServices.LoginAsync(loginDto);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            logger.LogInformation($"Registering User {userDto.Email}");
            var result = await authServices.RegisterAsync(userDto);
            return StatusCode((int)result.StatusCode, result);
        }


    }
}
