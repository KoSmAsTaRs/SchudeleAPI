namespace ScheduleServer.Dto;
using ScheduleServer.Models;
using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    public string name { get; set; } = null!;
    public string email { get; set; } = null!;
    public string password { get; set; } = null!;
    public int role_id { get; set; }

}