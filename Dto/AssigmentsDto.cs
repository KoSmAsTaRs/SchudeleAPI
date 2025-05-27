namespace ScheduleServer.Dto;
using ScheduleServer.Models;
public class AssignmentsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Teacher Teacher { get; set; } = null!;
    public Subject Subject { get; set; } = null!;
    public Group Group { get; set; }= null!;
    public DateOnly start_date { get; set; }
    public DateOnly end_date { get; set; }
}