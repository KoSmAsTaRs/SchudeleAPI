using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using ScheduleServer.Dto;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private readonly IScheduleService _scheduleService;
    private readonly IMapper _mapper;
    private readonly ILogger<SchedulesController> _logger;

    public SchedulesController(
        IScheduleService scheduleService,
        ILogger<SchedulesController> logger,
        IMapper mapper)
    {
        _scheduleService = scheduleService;
        _logger = logger;
        _mapper = mapper;
    }




    [HttpGet("GetAllSchedules")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SchudeleDTO>))]
    public async Task<IActionResult> GetAllSchedules()
    {
        try
        {
            var schedules = await _scheduleService.GetAllSchedulesAsync();
            return Ok(schedules);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting all schedules");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpGet("GetScheduleById/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SchudeleDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScheduleById(int id)
    {
        try
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(id);
            return schedule == null ? NotFound() : Ok(schedule);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting schedule with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpGet("GetSchedulesByGroup/{groupId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SchudeleDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSchedulesByGroup(int groupId)
    {
        try
        {
            var schedule = await _scheduleService.GetSchedulesByGroupAsync(groupId);
            return schedule == null ? NotFound() : Ok(schedule);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting schedule for group {groupId}");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpPost("CreateSchedule")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SchudeleDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSchedule([FromBody] ScheduleCreateDto dto)
    {
        try
        {
            var createdSchedule = await _scheduleService.CreateScheduleAsync(dto);
            return CreatedAtAction(nameof(GetScheduleById), new { id = createdSchedule.Id }, createdSchedule);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating schedule");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpPut("UpdateSchedule/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSchedule(int id, [FromBody] ScheduleUpdateDto dto)
    {
        try
        {
            await _scheduleService.UpdateScheduleAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating schedule with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }


    [HttpDelete("DeleteSchedule/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSchedule(int id)
    {
        try
        {
            await _scheduleService.DeleteScheduleAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting schedule with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("GetScheduleByTeacherId")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScheduleByTeacherName(string teacherName)
    {
        try
        {
            var schedule = await _scheduleService.GetSchudelByTeacherName(teacherName);
            if (schedule == null || !schedule.Any()) // Проверка на пустую коллекцию
            {
                _logger.LogInformation($"No schedule found for teacher ID: {teacherName}");
                return NotFound($"No schedule found for teacher ID: {teacherName}");
            }
            return schedule == null ? NotFound() : Ok(schedule);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting schedule with id {teacherName}");
            return StatusCode(500, "Internal server error");
        }
    }
}