namespace ScheduleServer.Dto;

using ScheduleServer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class SubjectDto
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public DepartmentDto Department { get; set; } = null!;

    public ICollection<AssignmentsDto> Assignment { get; set; } = new List<AssignmentsDto>();

    public SubjectDto() { }
}