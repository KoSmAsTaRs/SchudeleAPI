using System.ComponentModel.DataAnnotations;
namespace ScheduleServer.Models;

public class Assignments
{
    public int id { get; set; }
    public int teacher_id { get; set; }
    public Teacher teacher { get; set; } = null!;
    public int subject_id { get; set; }
    public Subject subject { get; set; } = null!;
    public int group_id { get; set; }
    public Group group { get; set; } = null!;
    public DateOnly start_date { get; set; }
    public DateOnly end_date { get; set; }
}