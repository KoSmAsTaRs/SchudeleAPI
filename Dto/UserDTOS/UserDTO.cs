namespace ScheduleServer.Dto;
using ScheduleServer.Models;
using System.ComponentModel.DataAnnotations;

public class UserDto
{
    public int id { get; set; }
    public string name { get; set; } = null!;
    public int role_id { get; set; } 
    public string email { get; set; } = null!;
}