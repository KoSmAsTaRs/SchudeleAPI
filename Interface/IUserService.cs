using ScheduleServer.Dto;

public interface IUserService
{
    Task<UserDto> CreateUserAsync(CreateUserDto dto);
    Task<UserDto> GetUserByIdAsync(int id);
    Task DeleteUserByIdAsync(int id);
    Task UpdateUserAsync(int id, UserUpdateDto dto);
}