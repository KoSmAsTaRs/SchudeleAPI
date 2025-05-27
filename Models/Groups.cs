using ScheduleServer.Models;

public class Group{
    public int id { get; set; }
    public string name { get; set; } = null!;
    public int department_id { get; set; }
    public Department department { get; set; } = null!;
    public ICollection<Assignments> assignments { get; set; } = null!;
}