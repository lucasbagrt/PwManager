using AutoMapper;
using PasswordManager.Model;
using PasswordManager.ValueObjects;

namespace PasswordManager.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<UserVO, User>().ReverseMap();
            config.CreateMap<PasswordVO, Password>().ReverseMap();
            config.CreateMap<ApplicationVO, Application>().ReverseMap();
        });
        return mappingConfig;
    }
}

