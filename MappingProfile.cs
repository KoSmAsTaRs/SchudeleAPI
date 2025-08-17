using AutoMapper;
using ScheduleServer.Models;
using ScheduleServer.Dto;
using ScheduleServer.Data;
using ScheduleServer;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Schedule, SchudeleDTO>()
            .ForMember(dest => dest.user_id,
                opt => opt.MapFrom(src => src.user_id))
            .ForMember(dest => dest.subject_id,
                opt => opt.MapFrom(src => src.subject_id))
            .ForMember(dest => dest.group_id,
                opt => opt.MapFrom(src => src.group_id))
            .ForMember(dest => dest.room_id,
                opt => opt.MapFrom(src => src.classroom_id))
            .ForMember(dest => dest.day_of_week,
                opt => opt.MapFrom(src => src.day_of_week))
            .ForMember(dest => dest.start_time,
                opt => opt.MapFrom(src => src.start_time))
            .ForMember(dest => dest.start_time,
                opt => opt.MapFrom(src => src.end_time));


        CreateMap<ScheduleCreateDto, Schedule>()
            .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.user_id))
            .ForMember(dest => dest.subject_id, opt => opt.MapFrom(src => src.subject_id))
            .ForMember(dest => dest.group_id, opt => opt.MapFrom(src => src.group_id))
            .ForMember(dest => dest.classroom_id, opt => opt.MapFrom(src => src.room_id))
            .ForMember(dest => dest.day_of_week, opt => opt.MapFrom(src => src.day_of_week))
            .ForMember(dest => dest.start_time, opt => opt.MapFrom(src => src.start_time))
            .ForMember(dest => dest.end_time, opt => opt.MapFrom(src => src.end_time))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.week_type))
            .ForMember(dest => dest.subject, opt => opt.Ignore())
            .ForMember(dest => dest.group, opt => opt.Ignore());


        CreateMap<ScheduleUpdateDto, Schedule>()
            .ForMember(dest => dest.user_id, opt => opt.MapFrom(src => src.user_id))
            .ForMember(dest => dest.subject_id, opt => opt.MapFrom(src => src.subject_id))
            .ForMember(dest => dest.group_id, opt => opt.MapFrom(src => src.group_id))
            .ForMember(dest => dest.classroom_id, opt => opt.MapFrom(src => src.room_id))
            .ForMember(dest => dest.day_of_week, opt => opt.MapFrom(src => src.day_of_week))
            .ForMember(dest => dest.start_time, opt => opt.MapFrom(src => src.start_time))
            .ForMember(dest => dest.end_time, opt => opt.MapFrom(src => src.end_time))
            .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.week_type))
            .ForMember(dest => dest.subject, opt => opt.Ignore())
            .ForMember(dest => dest.group, opt => opt.Ignore());

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.role_id, opt => opt.MapFrom(src => src.role_id))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email));

        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.role_id, opt => opt.MapFrom(src => src.role_id))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email))
            .ForMember(dest => dest.password_hash, opt => opt.MapFrom(src => src.password));

        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.role_id, opt => opt.MapFrom(src => src.role_id))
            .ForMember(dest => dest.email, opt => opt.MapFrom(src => src.email));

        CreateMap<Group, GroupDto>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.faculty, opt => opt.MapFrom(src => src.faculty));

        CreateMap<GroupDto, Group>()
            .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name))
            .ForMember(dest => dest.faculty, opt => opt.MapFrom(src => src.faculty));

    }
}

