using ScheduleServer.Dto;
public interface IScheduleService
{
    Task<IEnumerable<ScheduleDto>> GetAllSchedulesAsync();
    Task<ScheduleDto> GetScheduleByIdAsync(int id);
    Task<ScheduleDto> GetSchedulesByGroupAsync(int groupId);
    Task<ScheduleDto> CreateScheduleAsync(ScheduleCreateDto dto);
    Task UpdateScheduleAsync(int id, ScheduleUpdateDto dto);
    Task DeleteScheduleAsync(int id);
    Task <IEnumerable<ScheduleDto>> GetSchudelByTeacherId(int id);
}