using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cinema.API.Tools;
using Cinema.API.Tools.Interfaces;
using Cinema.Services.Dtos;
using Cinema.Services.Exceptions;
using Cinema.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
            try
            {
                var userDto = await _userService.CreateUser(authDto);
                return Ok(
                    new
                    {
                        token = _authTool.CreateToken(authDto.Email, userDto.Id, userDto.Role)
                    }
                );
            }
            catch
            { 
                return StatusCode(500, new
                {
                    message = "Such user already exists:("
                });
            }
        }
        
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            try
            {
                var authHeaderData = AuthenticationHeaderValue.Parse(HttpContext.Request.Headers["Authorization"]);
                if (!authHeaderData.Scheme.Equals("Basic"))
                {
                    return BadRequest(new
                    {
                        message = "Authorization schema is not supported"
                    });
                }

                var authCredentials = Encoding.ASCII
                    .GetString(Convert.FromBase64String(authHeaderData.Parameter)).Split(":");
                var authDto = new AuthDto()
                {
                    Email = authCredentials[0],
                    Password = authCredentials[1]
                };
                var userDto = await _userService.CheckAuthData(authDto);
                return Ok(new
                {
                    token = _authTool.CreateToken(authDto.Email, userDto.Id, userDto.Role)
                });
            }
            catch (AuthenticationDataInvalidException e)
            {
                return NotFound(new
                {
                    message = e.Message
                });
            }
            catch (NullReferenceException e)
            {
                return BadRequest(new
                {
                    message = "No authorization data"
                });
            }
        }
    }
}