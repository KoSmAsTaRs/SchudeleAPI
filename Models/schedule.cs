using System.Text.RegularExpressions;
namespace ScheduleServer.Models;
public class Schedule{
    public int id { get; set; }
    public string? room { get; set; }
    public TimeOnly start_time { get; set; }
    public TimeOnly end_time { get; set; }
    public string? day_of_week { get; set; }
    public string? week_type { get; set; }
    //Внешние ключи
    public int teacher_id { get; set; }
    public int subject_id { get; set; }
    public int group_id { get; set; }
    //Навигационные свойства
    public Teacher teacher { get; set; }
    public Subject subject { get; set; } = null!;
    public Group group { get; set; } = null!;
   
}


