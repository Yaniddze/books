using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksApi.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(options =>
            {
                options.AddExpressionMapping();

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
                        ));

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
            }, typeof(Startup));
        }
    }
}