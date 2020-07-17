using AutoMapper;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;

namespace BooksApi.Installers.AutoMapper.Mappers
{
    public class BookMapperInstaller : IMapperInstaller
    {
        public void InstallMapper(IMapperConfigurationExpression options)
        {
            options.CreateMap<BookDB, Book>()
                .ForMember(x => x.Author,
                    map => map.MapFrom(
                        dest => new Author
                        {
                            Id = dest.Author.Id,
                            Name = dest.Author.Name,
                        }
                    ))
                .ForMember(x => x.Genre,
                    map => map.MapFrom(
                        dest => new Genre
                        {
                            Id = dest.Genre.Id,
                            Title = dest.Genre.Title,
                        }
                    ))
                .ForMember(x => x.BookInfo,
                    map => map.MapFrom(
                        dest => new BookInfo
                        {
                            Title = dest.Title,
                            Year = dest.Year,
                        }
                    ))
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ));

            options.CreateMap<Book, BookDB>()
                .ForMember(x => x.Author,
                    map => map.MapFrom(
                        dest => new AuthorDB
                        {
                            Id = dest.Author.Id,
                            Name = dest.Author.Name,
                        }
                    ))
                .ForMember(x => x.Genre,
                    map => map.MapFrom(
                        dest => new GenreDB
                        {
                            Id = dest.Genre.Id,
                            Title = dest.Genre.Title,
                        }
                    ))
                .ForMember(x => x.Title,
                    map => map.MapFrom(
                        dest => dest.BookInfo.Title
                    ))
                .ForMember(x => x.Year,
                    map => map.MapFrom(
                        dest => dest.BookInfo.Year
                    ))
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ))
                .ForMember(x => x.AuthorId,
                    map => map.MapFrom(
                        dest => dest.Author.Id
                    ))
                .ForMember(x => x.GenreId,
                    map => map.MapFrom(
                        dest => dest.Genre.Id
                    ));
        }
    }
}