using AutoMapper;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;

namespace BooksApi.Installers.AutoMapper.Mappers
{
    public class GenreMapperInstaller: IMapperInstaller
    {
        public void InstallMapper(IMapperConfigurationExpression options)
        {
            options.CreateMap<GenreDB, Genre>()
                .ForMember(x => x.Title,
                    map => map.MapFrom(
                        dest => dest.Title
                    ))
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ));

            options.CreateMap<Genre, GenreDB>()
                .ForMember(x => x.Title,
                    map => map.MapFrom(
                        dest => dest.Title
                    ))
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ));
        }
    }
}