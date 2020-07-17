using System;
using AutoMapper;
using BooksApi.CQRS.Commands;
using BooksApi.UseCases.UpdateBook;

namespace BooksApi.Installers.AutoMapper.Mappers
{
    public class UpdateBookRequestMapperInstaller: IMapperInstaller
    {
        public void InstallMapper(IMapperConfigurationExpression options)
        {
            options.CreateMap<UpdateBookRequest, UpdateBookCommand>()
                .ForMember(x => x.BookId,
                    map => map.MapFrom(
                        dest => Guid.Parse(dest.BookId)
                    ))
                .ForMember(x => x.NewAuthorId,
                    map => map.MapFrom(
                        dest => Guid.Parse(dest.NewAuthorId)
                    ))
                .ForMember(x => x.NewGenreId,
                    map => map.MapFrom(
                        dest => Guid.Parse(dest.NewGenreId)
                    ))
                .ForMember(x => x.NewTitle,
                    map => map.MapFrom(
                        dest => dest.NewTitle
                    ))
                .ForMember(x => x.NewYear,
                    map => map.MapFrom(
                        dest => dest.NewYear
                    ));

            options.CreateMap<UpdateBookCommand, UpdateBookRequest>()
                .ForMember(x => x.BookId,
                    map => map.MapFrom(
                        dest => dest.BookId
                        ))
                .ForMember(x => x.NewTitle,
                map => map.MapFrom(
                    dest => dest.NewTitle
                    ))
                .ForMember(x => x.NewYear,
                    map => map.MapFrom(
                        dest => dest.NewYear
                        ))
                .ForMember(x => x.NewAuthorId,
                    map => map.MapFrom(
                        dest => dest.NewAuthorId
                        ))
                .ForMember(x => x.NewGenreId,
                    map => map.MapFrom(
                        dest => dest.NewGenreId
                        ));
        }
    }
}