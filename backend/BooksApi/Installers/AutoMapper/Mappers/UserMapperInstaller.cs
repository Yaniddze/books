using AutoMapper;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;

namespace BooksApi.Installers.AutoMapper.Mappers
{
    public class UserMapperInstaller: IMapperInstaller
    {
        public void InstallMapper(IMapperConfigurationExpression options)
        {
            options.CreateMap<User, UserDB>()
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ))
                .ForMember(x => x.Login,
                    map => map.MapFrom(
                        dest => dest.LoginInfo.Login
                    ))
                .ForMember(x => x.Password,
                    map => map.MapFrom(
                        dest => dest.LoginInfo.Password
                    ));

            options.CreateMap<UserDB, User>()
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ))
                .ForMember(x => x.LoginInfo,
                    map => map.MapFrom(
                        dest => new LoginInfo
                        {
                            Login = dest.Login, 
                            Password = dest.Password,
                        }
                    ));
        }
    }
}