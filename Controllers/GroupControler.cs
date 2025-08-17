using ScheduleServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using ScheduleServer.Dto;


[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;
    private readonly ShcheduleContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<GroupController> _logger;
    public GroupController(
         IGroupService groupService,
         ShcheduleContext context,
         ILogger<GroupController> logger,
         IMapper mapper)
    {
        _groupService = groupService;
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet("GetGroupById")]    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetGroupById(int id)
    {
        try
        {
            var group = await _groupService.GetGroupByIdAsync(id);
            return group == null! ? NotFound() : Ok(group);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting group with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpPost("CreateGroup")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GroupDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGroupAsunc([FromBody] GroupDto dto)
    {
        try
        {
            var createGroup = await _groupService.CreateGroupAsync(dto);
            return CreatedAtAction(nameof(GetGroupById), new { createGroup.id }, createGroup);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error create group");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("DeleteUserById/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        try
        {
            await _groupService.DeleteGroupAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error delete user");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("UpdateUser/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSchedule(int id, [FromBody] GroupDto dto)
    {
        try
        {
            await _groupService.UpdateGroupAsync(dto,id);
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
            _logger.LogError(ex, $"Error updating user with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }
}