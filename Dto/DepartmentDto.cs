namespace ScheduleServer.Dto;
using ScheduleServer.Models;
public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}