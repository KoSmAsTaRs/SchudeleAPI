using System.ComponentModel.DataAnnotations;

namespace ScheduleServer.Dto;
public class ScheduleCreateDto
{
    [Required(ErrorMessage = "Teacher name is required")]
    public int user_id { get; set; } 

    [Required(ErrorMessage = "Subject name is required")]
    public int subject_id { get; set; } 

    [Required(ErrorMessage = "Group name is required")]
    public int group_id { get; set; } 

    [Required(ErrorMessage = "Room number is required")]
    public int room_id { get; set; } 

    [Required(ErrorMessage = "Day of week is required")]
    public string day_of_week { get; set; } = null!;

    [Required(ErrorMessage = "Start time is required")]
    public DateTime start_time { get; set; }

    [Required(ErrorMessage = "End time is required")]
    public DateTime end_time { get; set; }

    [Required(ErrorMessage = "Week type is required")]
    public string week_type { get; set; } = null!;
}