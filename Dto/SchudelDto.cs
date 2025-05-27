namespace ScheduleServer.Dto;
using ScheduleServer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public class ScheduleDto
{
    public int Id { get; set; }
    [Required]
    public string TeacherName { get; set; }
    [Required]
    public string SubjectName { get; set; }
    [Required]
    public string GroupName { get; set; }
    [Required]
    public string Room { get; set; }
    public TimeOnly start_time { get; set; }
    public TimeOnly end_time { get; set; }
    [Required]
    public string day_of_week { get; set; }
}