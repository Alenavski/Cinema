using System;
using CinemaAPI.Tools;
using CinemaServices.DTOs;
using CinemaServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CinemaAPI.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(AuthDTO authDto)
        {
            try
            {
                var userDto = _userService.CreateUser(authDto);
                return Ok(
                    new
                    {
                        token = AuthTool.CreateToken(authDto.Email, userDto.Id, userDto.Role)
                    }
                );
            }
            catch (SqlException)
            {
                return StatusCode(500, new
                {
                    message = "Internal server error, try again later"
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
                var userDto = _userService.CheckAuthData(authDto);
                return Ok(new
                {
                    token = AuthTool.CreateToken(authDto.Email, userDto.Id, userDto.Role)
                });
            }
            catch (SqlException)
            {
                return StatusCode(500, new
                {
                    message = "Internal server error, try again later"
                });
            }
            catch (Exception e)
            {
                return NotFound(new
                {
                    message = e.Message
                });
            }
        }
    }
}