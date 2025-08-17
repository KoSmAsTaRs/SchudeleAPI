using Microsoft.EntityFrameworkCore;
using ScheduleServer.Models;
using ScheduleServer.Dto;
using AutoMapper;
using ScheduleServer.Data;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class GroupService : IGroupService
{
    private readonly ShcheduleContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ScheduleService> _logger;

    public GroupService(
        ShcheduleContext context,
        IMapper mapper,
        ILogger<ScheduleService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<GroupDto> CreateGroupAsync(GroupDto dto)
    {
        var groupcreate = _mapper.Map<Group>(dto);
        if (await _context.groups.AnyAsync(g => g.name == dto.name))
            throw new ArgumentException("This group already exist");
        await _context.groups.AddAsync(groupcreate);
        await _context.SaveChangesAsync();

        return _mapper.Map<GroupDto>(groupcreate);
    }

    public async Task<GroupDto> GetGroupByIdAsync(int id)
    {
        var group = await _context.groups.FindAsync(id);
        if (group == null)
        {
            return null;
        }
        return _mapper.Map<GroupDto>(group);
    }

    public async Task DeleteGroupAsync(int id)
    {
        var group = await _context.groups.FindAsync(id);
        if (group == null)
        {
            return;
        }
        _context.groups.Remove(group);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateGroupAsync(GroupDto dto, int id)
    {
        var groupupdate = await _context.users.FirstOrDefaultAsync(g => g.id == id);
        if (groupupdate == null)
        {
            throw new KeyNotFoundException($"Group with {id} not found");
        }
        _mapper.Map(dto, groupupdate);
        
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