using Microsoft.EntityFrameworkCore;
using ScheduleServer.Models;
using ScheduleServer.Dto;
using AutoMapper;
using ScheduleServer.Data;
using AutoMapper.QueryableExtensions;

public class ScheduleService : IScheduleService
{
    private readonly ShcheduleContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<ScheduleService> _logger;

    public ScheduleService(
        ShcheduleContext context,
        IMapper mapper,
        ILogger<ScheduleService> logger)
    {
        _context = context;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<ScheduleDto>> GetAllSchedulesAsync()
    {
        var schedules = await _context.schedule
        .Include(s => s.subject)
        .Include(s => s.teacher)
        .Include(s => s.group)
        .AsNoTracking()
        .ToListAsync();
        return _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
    }

    public async Task<ScheduleDto> GetScheduleByIdAsync(int id)
    {
        var schedule = await _context.schedule
        .Include(s => s.subject)
        .Include(s => s.teacher)
        .Include(s => s.group)
        .AsNoTracking()
        .ProjectTo<ScheduleDto>(_mapper.ConfigurationProvider)
        .FirstOrDefaultAsync();

        if (schedule == null)
        {
            return null;
        }

        return _mapper.Map<ScheduleDto>(schedule);
    }


    public async Task<ScheduleDto> GetSchedulesByGroupAsync(int group_id)
    {
        var schedule = await _context.schedule
            .Where(s => s.group_id == group_id)
            .Include(s => s.subject)
            .Include(s => s.teacher)
            .Include(s => s.group)
            .AsNoTracking()
            .ProjectTo<ScheduleDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (schedule == null)
        {
            return null;
        }

        return _mapper.Map<ScheduleDto>(schedule);
    }

    public async Task<ScheduleDto> CreateScheduleAsync(ScheduleCreateDto dto)
    {
        var schedule = _mapper.Map<Schedule>(dto);
        if (!await _context.teachers.AnyAsync(t => t.id == dto.teacher_id))
            throw new ArgumentException("Teacher not found");

        if (!await _context.subjects.AnyAsync(s => s.id == dto.subject_id))
            throw new ArgumentException("Subject not found");

        if (!await _context.groups.AnyAsync(g => g.id == dto.group_id))
            throw new ArgumentException("Group not found");

        if (dto.start_time >= dto.end_time)
            throw new ArgumentException("End time must be after start time");

        if (!await _context.teachers.AnyAsync(t => t.id == dto.teacher_id))
            throw new ArgumentException("Teacher not found");

        await _context.schedule.AddAsync(schedule);
        await _context.SaveChangesAsync();

        return _mapper.Map<ScheduleDto>(schedule);
    }

    public async Task UpdateScheduleAsync(int id, ScheduleUpdateDto dto)
    {
        var schedule = await _context.schedule
            .Include(s => s.teacher)
            .Include(s => s.subject)
            .FirstOrDefaultAsync(s => s.id == id);

        if (schedule == null)
            throw new KeyNotFoundException($"Schedule with id {id} not found");

        if (dto.start_time >= dto.end_time)
            throw new ArgumentException("End time must be after start time");

        _mapper.Map(dto, schedule);

        if (dto.teacher_id.HasValue)
            schedule.teacher = await _context.teachers.FindAsync(dto.teacher_id.Value);

        if (_context.Entry(schedule).State == EntityState.Unchanged)
            return;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error updating schedule with id {ScheduleId}", id);
            throw new InvalidOperationException("Database update failed");
        }
    }

    public async Task DeleteScheduleAsync(int id)
    {
        var schedule = await _context.schedule.FindAsync(id);
        if (schedule == null)
            return;

        _context.schedule.Remove(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ScheduleDto>> GetSchudelByTeacherId(int id)
    {
        var schedule = await _context.schedule
            .Where(s => s.teacher_id == id)
            .Include(s => s.subject)
            .Include(s => s.teacher)
            .Include(s => s.group)
            .AsNoTracking()
            .ProjectTo<ScheduleDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return _mapper.Map<IEnumerable<ScheduleDto>>(schedule);
    }
}