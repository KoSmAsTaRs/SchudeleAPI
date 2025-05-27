namespace ScheduleServer.Dto;
using ScheduleServer.Models;
public class GroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public Department Department { get; set; }= null!;
    public ICollection<Assignments> Assignments { get; set; }= null!;
}