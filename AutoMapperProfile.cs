using AutoMapper;
using template_api.Dtos.User;

namespace template_api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, LoadUserDto>();
        CreateMap<AddUserDto, User>();
        CreateMap<User, AddUserDto>();
        CreateMap<AddUserDto, LoadUserDto>();
    }
}

