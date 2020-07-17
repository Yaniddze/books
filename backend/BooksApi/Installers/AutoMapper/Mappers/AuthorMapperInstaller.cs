using AutoMapper;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;

namespace BooksApi.Installers.AutoMapper.Mappers
{
    public class AuthorMapperInstaller: IMapperInstaller
    {
        public void InstallMapper(IMapperConfigurationExpression options)
        {
            options.CreateMap<AuthorDB, Author>()
                .ForMember(x => x.Name,
                    map => map.MapFrom(
                        dest => dest.Name
                    ))
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ));

            options.CreateMap<Author, AuthorDB>()
                .ForMember(x => x.Name,
                    map => map.MapFrom(
                        dest => dest.Name
                    ))
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ));
        }
    }
}