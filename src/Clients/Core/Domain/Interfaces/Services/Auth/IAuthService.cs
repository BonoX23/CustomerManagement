using Domain.DTOs;

namespace Domain.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> AutenticateAsync(AuthDto authDto);
        Task UpdateUserPasswordAsync(int userId, UserDto userDto);
    }
}
