namespace ScheduleServer.Models;

public class Schedule
{
    public int id { get; set; }
    public DateTime start_time { get; set; }
    public DateTime end_time { get; set; }
    public string day_of_week { get; set; } = null!;
    public string type { get; set; } = null!;
    //Внешние ключи
    public int user_id { get; set; }
    public int subject_id { get; set; }
    public int group_id { get; set; }
    public int classroom_id { get; set; }
    //Навигационные свойства
    public Subject subject { get; set; } = null!;
    public Group group { get; set; } = null!;
    public Classrooms room { get; set; } = null!;
    public User users { get; set; } = null!;
   
}


