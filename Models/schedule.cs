namespace ScheduleServer.Models;

public class Schedule
{
    public int id { get; set; }
<<<<<<< HEAD

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
=======
    public string? room { get; set; }
    public TimeOnly start_time { get; set; }
    public TimeOnly end_time { get; set; }
    public string? day_of_week { get; set; }
    public string? week_type { get; set; }
    public int teacher_id { get; set; }
    public int subject_id { get; set; }
    public int group_id { get; set; }
    public Teacher teacher { get; set; }
>>>>>>> d4d2a9f263873f62452ffea7b068b854e5fc26ef
    public Subject subject { get; set; } = null!;
    public Group group { get; set; } = null!;
    public Classrooms room { get; set; } = null!;
    public User users { get; set; } = null!;
   
}


