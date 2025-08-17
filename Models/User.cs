using System.ComponentModel.DataAnnotations;
using ScheduleServer.Models;

namespace ScheduleServer;

public class User
{
    public int id { get; set; }
    public string name { get; set; } = null!;
    
    [EmailAddress(ErrorMessage = "Некорректный адрес")] public string email { get; set; } = null!;
    public string password_hash { get; set; } = null!;
    public int role_id { get; set; }
    public Role role { get; set; } = null!;
}