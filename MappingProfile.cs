using AutoMapper;
using ScheduleServer.Models;
using ScheduleServer.Dto;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Schedule, ScheduleDto>()
            .ForMember(dest => dest.TeacherName,
                opt => opt.MapFrom(src => src.teacher.name)) 
            .ForMember(dest => dest.SubjectName,
                opt => opt.MapFrom(src => src.subject.name))
            .ForMember(dest => dest.GroupName,
                opt => opt.MapFrom(src => src.group.name))
            .ForMember(dest => dest.Room,
                opt => opt.MapFrom(src => src.room))
            .ForMember(dest => dest.day_of_week,
                opt => opt.MapFrom(src => src.day_of_week)) 
            .ForMember(dest => dest.start_time,
                opt => opt.MapFrom(src => src.start_time)) 
            .ForMember(dest => dest.start_time,
                opt => opt.MapFrom(src => src.end_time));
        

        CreateMap<ScheduleCreateDto, Schedule>()
            .ForMember(dest => dest.teacher_id, opt => opt.MapFrom(src => src.teacher_id))
            .ForMember(dest => dest.subject_id, opt => opt.MapFrom(src => src.subject_id))
            .ForMember(dest => dest.group_id, opt => opt.MapFrom(src => src.group_id))
            .ForMember(dest => dest.room, opt => opt.MapFrom(src => src.room))
            .ForMember(dest => dest.day_of_week, opt => opt.MapFrom(src => src.day_of_week))
            .ForMember(dest => dest.start_time, opt => opt.MapFrom(src => src.start_time))
            .ForMember(dest => dest.end_time, opt => opt.MapFrom(src => src.end_time))
            .ForMember(dest => dest.week_type, opt => opt.MapFrom(src => src.week_type))
            .ForMember(dest => dest.teacher, opt => opt.Ignore()) 
            .ForMember(dest => dest.subject, opt => opt.Ignore())
            .ForMember(dest => dest.group, opt => opt.Ignore());


        CreateMap<ScheduleUpdateDto, Schedule>()
            .ForMember(dest => dest.teacher_id, opt => opt.MapFrom(src => src.teacher_id))
            .ForMember(dest => dest.subject_id, opt => opt.MapFrom(src => src.subject_id))
            .ForMember(dest => dest.group_id, opt => opt.MapFrom(src => src.group_id))
            .ForMember(dest => dest.room, opt => opt.MapFrom(src => src.room))
            .ForMember(dest => dest.day_of_week, opt => opt.MapFrom(src => src.day_of_week))
            .ForMember(dest => dest.start_time, opt => opt.MapFrom(src => src.start_time))
            .ForMember(dest => dest.end_time, opt => opt.MapFrom(src => src.end_time))
            .ForMember(dest => dest.week_type, opt => opt.MapFrom(src => src.week_type))
            .ForMember(dest => dest.teacher, opt => opt.Ignore())
            .ForMember(dest => dest.subject, opt => opt.Ignore())
            .ForMember(dest => dest.group, opt => opt.Ignore());

        // Маппинг для других сущностей
        CreateMap<Assignments, AssignmentsDto>();
        CreateMap<Department, DepartmentDto>();
        CreateMap<Group, GroupDto>();
        CreateMap<Subject, SubjectDto>();
        CreateMap<Teacher, TeacherDto>();

        // Обратный маппинг (если нужен)
        CreateMap<ScheduleDto, Schedule>().ReverseMap();
    }
}