using System.ComponentModel.DataAnnotations;
using ScheduleServer.Models;

namespace ScheduleServer.Dto;
public class ScheduleCreateDto
{
    [Required(ErrorMessage = "TeacherId is required")]
    public int teacher_id { get; set; }

    [Required(ErrorMessage = "SubjectId is required")]
    public int subject_id { get; set; }

    [Required(ErrorMessage = "GroupId is required")]
    public int group_id { get; set; }

    [Required(ErrorMessage = "Room number is required")]
    public string room { get; set; }

    [Required(ErrorMessage = "Day of week is required")]
    public string day_of_week { get; set; }

    [Required(ErrorMessage = "Start time is required")]
    public TimeOnly start_time { get; set; }

    [Required(ErrorMessage = "End time is required")]
    public TimeOnly end_time { get; set; }

    [Required(ErrorMessage = "Week type is required")]
    public string? week_type { get; set; }
}