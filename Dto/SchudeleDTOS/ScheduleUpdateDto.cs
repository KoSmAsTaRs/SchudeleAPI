namespace ScheduleServer.Dto;
using ScheduleServer.Models;
public class ScheduleUpdateDto
{
    public int? user_id { get; set; }
    public int subject_id { get; set; }
    public int group_id { get; set; }
    public int room_id { get; set; }
    public string? day_of_week { get; set; }
    public DateTime? start_time { get; set; }
    public DateTime? end_time { get; set; }
    public string? week_type { get; set; }
}