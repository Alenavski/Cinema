using System.Threading.Tasks;
using Cinema.API.Tools.Interfaces;
using Cinema.Services.Dtos;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthTool _authTool;

        public UserController(IUserService userService, IAuthTool authTool)
        {
            _userService = userService;
            _authTool = authTool;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthDto authDto)
        {
            var userEntity = await _userService.FindUserByEmail(authDto);
            if (userEntity != null)
            {
                return BadRequest(new
                {
                    message = "User with this email already exists:("
                });
            }
            var userDto = await _userService.CreateUser(authDto);
            return Ok(
                new
                {
                    token = _authTool.CreateToken(authDto.Email, userDto.Id, userDto.Role.ToString())
                }
            );    
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthDto authDto)
        {
            var userEntity = await _userService.FindUserByEmail(authDto);
            if (userEntity == null)
            {
                return Unauthorized(new
                {
                    message = "User with this email wasn't found:("
                });
            }
            var userDto = _userService.CheckPassword(userEntity, authDto);
            if (userDto == null)
            {
                return Unauthorized(new
                {
                    message = "Password is incorrect:("
                });
            }
            return Ok(new
            {
                token = _authTool.CreateToken(authDto.Email, userDto.Id, userDto.Role.ToString())
            });
        }
    }
}