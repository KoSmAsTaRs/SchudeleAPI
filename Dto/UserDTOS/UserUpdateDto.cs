namespace ScheduleServer.Dto;


public class UserUpdateDto
{
    public int id { get; set; }
    public string name { get; set; } = null!;
    public int role_id { get; set; } 
    public string email { get; set; } = null!;
}