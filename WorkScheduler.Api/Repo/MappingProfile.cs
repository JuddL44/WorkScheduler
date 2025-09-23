using AutoMapper;

namespace WorkScheduler.Api.Repo
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShiftCreateDTO, Shift>();
            CreateMap<ShiftUpdateDTO, Shift>();
            CreateMap<UserUpdateDTO, User>();
            CreateMap<UserCreateDTO, User>();
            CreateMap<Shift, ShiftCreateDTO>();
            CreateMap<Shift, ShiftUpdateDTO>();
            CreateMap<User, UserUpdateDTO>();
            CreateMap<User, UserCreateDTO>();
            CreateMap<User, UserDisplayDTO>();
            CreateMap<UserDisplayDTO, User>();
        }
    }
}
