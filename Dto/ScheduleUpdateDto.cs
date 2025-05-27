namespace ScheduleServer.Dto;
using ScheduleServer.Models;
public class ScheduleUpdateDto
{
    public int? teacher_id { get; set; }
    public int? subject_id { get; set; }
    public int? group_id { get; set; }
    public string? room { get; set; }
    public string day_of_week { get; set; }
    public TimeOnly? start_time { get; set; }
    public TimeOnly? end_time { get; set; }
    public string? week_type { get; set; }
}