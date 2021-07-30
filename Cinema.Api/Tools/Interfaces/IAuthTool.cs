using Cinema.Services.Dtos;

namespace Cinema.Api.Tools.Interfaces
{
    public interface IAuthTool
    {
        string CreateToken(UserDto userDto);
    }
}