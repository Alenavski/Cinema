using System.Threading.Tasks;
using Cinema.DB.Entities;
using Cinema.Services.Dtos;

namespace Cinema.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> CreateUser(AuthDto authDto);
        Task<UserEntity> FindUserByEmail(AuthDto authDto);
        UserDto CheckPassword(UserEntity user, AuthDto authDto);
    }
}