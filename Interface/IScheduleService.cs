using ScheduleServer.Dto;
public interface IScheduleService
{
    Task<IEnumerable<SchudeleDTO>> GetAllSchedulesAsync();
    Task<SchudeleDTO> GetScheduleByIdAsync(int id);
    Task<SchudeleDTO> GetSchedulesByGroupAsync(int groupId);
    Task<SchudeleDTO> CreateScheduleAsync(ScheduleCreateDto dto);
    Task UpdateScheduleAsync(int id, ScheduleUpdateDto dto);
    Task DeleteScheduleAsync(int id);
    Task <IEnumerable<SchudeleDTO>> GetSchudelByTeacherName(string name);
}