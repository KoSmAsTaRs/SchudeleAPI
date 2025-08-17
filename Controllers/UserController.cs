using ScheduleServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using ScheduleServer.Dto;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ShcheduleContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    public UserController(
         IUserService userService,
         ShcheduleContext context,
         ILogger<UserController> logger,
         IMapper mapper)
    {
        _userService = userService;
        _logger = logger;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet("GetUserById/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user == null! ? NotFound() : Ok(user);
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error getting schedule with id {id}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost("CreateUser")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUserAsunc([FromBody] CreateUserDto dto)
    {
        try
        {
            var createUser = await _userService.CreateUserAsync(dto);
            return CreatedAtAction(nameof(GetUserById), new { createUser.id }, createUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error create user");
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
            await _userService.DeleteUserByIdAsync(id);
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
    public async Task<IActionResult> UpdateSchedule(int id, [FromBody] UserUpdateDto dto)
    {
        try
        {
            await _userService.UpdateUserAsync(id,dto);
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
 