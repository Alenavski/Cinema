using System.Threading.Tasks;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(AuthDto authDto);
        Task<UserDto> CheckAuthData(AuthDto authDto);
    }
}