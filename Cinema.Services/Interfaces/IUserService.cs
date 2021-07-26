using CinemaServices.DTOs;

namespace CinemaServices.Interfaces
{
    public interface IUserService
    {
        UserDTO CreateUser(AuthDTO authDto);
        UserDTO CheckAuthData(AuthDTO authDto);
    }
}