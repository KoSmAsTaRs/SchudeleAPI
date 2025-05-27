using System.ComponentModel.DataAnnotations;

namespace ScheduleServer.Models;

public class Department
{
    public int id { get; set; }
    public string name { get; set; } = null!;
    public ICollection<Teacher> teachers { get; set; } = new List<Teacher>();
    public ICollection<Group> groups { get; set; } = new List<Group>();
    [Required]
    public ICollection<Subject> subjects { get; set; } = new List<Subject>();
}