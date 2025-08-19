using Microsoft.AspNetCore.Mvc;
using AT2CityLinkAPI.Models;
using AT2CityLinkAPI.Services;
using BCrypt.Net;

namespace AT2CityLinkAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister dto)
        {
            var existingUser = await _userService.GetByUsernameAsync(dto.Username);
            if (existingUser != null)
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _userService.CreateUserAsync(user);
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin dto)
        {
            var user = await _userService.GetByUsernameAsync(dto.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok("Login successful.");
        }
    }
}