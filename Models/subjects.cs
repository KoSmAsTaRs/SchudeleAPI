using ScheduleServer.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ScheduleServer.Models;

public class Subject
{
    public int id { get; set; }
    [Required]
    public string? name { get; set; }
    public string? description { get; set; }
}