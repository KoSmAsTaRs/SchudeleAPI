using System.ComponentModel.DataAnnotations;
namespace ScheduleServer.Models;

public class Teacher{
    public int id { get; set; }
    [Required]
    [StringLength(50)]
    public string name { get; set; } 
    public int department_id { get; set; }
    public Department department { get; set; } = null!;
    public ICollection<Assignments> assignments { get; set; }= null!;
}