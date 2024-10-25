using AutoMapper;
using template_api.Dtos.Client;
using template_api.Dtos.User;
using template_api.Dtos.UserSetting;

namespace template_api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, LoadUserDto>();
        CreateMap<AddUserDto, User>();
        CreateMap<User, AddUserDto>();
        CreateMap<Admin, AddUserDto>();
        CreateMap<Trainer, AddUserDto>();
        CreateMap<Client, AddUserDto>();
        CreateMap<Admin, LoadUserDto>();
        CreateMap<Trainer, LoadUserDto>();
        CreateMap<Client, LoadUserDto>();
        CreateMap<AddUserDto, Admin>();
        CreateMap<AddUserDto, Trainer>();
        CreateMap<AddUserDto, Client>();
        CreateMap<Client, LoadClientDto>();
        CreateMap<AddClientDto, Client>();
        CreateMap<UpdateClientDto, Client>();
        CreateMap<UpdateClientDto, LoadClientDto>();
        CreateMap<Client, UpdateClientDto>();
        CreateMap<AddUserDto, LoadUserDto>();
        CreateMap<SettingsDto, UserSettings>();
        CreateMap<UserSettings, SettingsDto>();
    }
}

