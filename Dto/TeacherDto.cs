namespace ScheduleServer.Dto;
using ScheduleServer.Models;
using System.ComponentModel.DataAnnotations;
public class TeacherDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }=null!;
    public string LastName { get; set; }=null!;
    public string? Patronymic { get; set; }
    public Department Department { get; set; }=null!;
}