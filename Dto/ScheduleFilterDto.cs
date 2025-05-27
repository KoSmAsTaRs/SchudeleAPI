namespace ScheduleServer.Dto;
public class ScheduleFilterDto
{
    public int? GroupId { get; set; }
    public int? TeacherId { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public DateTime? Date { get; set; }
}