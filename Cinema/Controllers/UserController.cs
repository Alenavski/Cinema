using System;
using Cinema.Tools;
using CinemaServices;
using CinemaServices.ModelsDTO;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        [HttpPost("register")]
        public IActionResult Register(AuthDTO authDto)
        {
            try
            {
                authDto = _userService.CreateUser(authDto);
                return Ok(new
                {
                    token = AuthTool.CreateToken(authDto.Email, authDto.Id, authDto.Role),
                    role = authDto.Role,
                    id = authDto.Id
                });
            }
            catch
            {
                return StatusCode(500, new
                {
                    message = "Such user already exists:("
                });
            }
        }
        
        [HttpPut("login")]
        public IActionResult Login(AuthDTO authDto)
        {
            try
            {
                authDto = _userService.CheckAuthData(authDto);
                return Ok(new
                {
                    token = AuthTool.CreateToken(authDto.Email, authDto.Id, authDto.Role),
                    role = authDto.Role,
                    id = authDto.Id
                });
            }
            catch (Exception e)
            {
                return StatusCode(404, new
                {
                    message = e.Message
                });
            }
        }
    }
}