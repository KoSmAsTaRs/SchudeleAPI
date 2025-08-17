using ScheduleServer.Data;
using AutoMapper;
using ScheduleServer.Dto;
using ScheduleServer;
using Microsoft.EntityFrameworkCore;


public class UserService : IUserService
{
    private readonly ShcheduleContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly IPasswordHasher _passwordhasher;
    public UserService(
         ShcheduleContext context,
         ILogger<UserController> logger,
         IMapper mapper,
         IPasswordHasher passwordHasher)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _passwordhasher = passwordHasher;
    }

    public async Task<UserDto> GetUserByIdAsync(int id)
    {
        var user = await _context.users.FindAsync(id);
        if (user == null)
        {
            return null;
        }

        return _mapper.Map<UserDto>(user);
    }
    public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.password_hash = _passwordhasher.HashPassword1(dto.password);

        await _context.users.AddAsync(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task DeleteUserByIdAsync(int id)
    {
        var user = await _context.users.FindAsync(id);
        if (user == null)
        {
            return;
        }
        _context.users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(int id, UserUpdateDto dto)
    {
        var updateUser = await _context.users.FirstOrDefaultAsync(u => u.id == id);
        if (updateUser == null)
        {
            throw new KeyNotFoundException($"User with {id} not found");
        }
        _mapper.Map(dto, updateUser);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, $"Error updating schedule with id {id}", id);
            throw new InvalidOperationException("Database update failed");
        }
    }
}