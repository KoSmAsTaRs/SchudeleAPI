namespace ScheduleServer.Dto;
using ScheduleServer.Models;
using System.ComponentModel.DataAnnotations;

public class SchudeleDTO
{
    public int Id { get; set; }
    [Required]
    public int user_id { get; set; } 
    [Required]
    public int subject_id { get; set; } 
    [Required]
    public int group_id { get; set; } 
    [Required]
    public int room_id { get; set; } 
    public DateTime start_time { get; set; }
    public DateTime end_time { get; set; }
    [Required]
    public string day_of_week { get; set; } = null!;
}