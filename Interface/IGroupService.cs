using ScheduleServer.Dto;

public interface IGroupService
{
    Task<GroupDto> CreateGroupAsync(GroupDto dto);
    Task<GroupDto> GetGroupByIdAsync(int id);
    Task DeleteGroupAsync(int id);
    Task UpdateGroupAsync(GroupDto dto, int id);
}