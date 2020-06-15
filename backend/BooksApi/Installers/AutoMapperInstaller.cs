using System;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using BooksApi.CQRS.Commands;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;
using BooksApi.UseCases.UpdateBook;
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
                        ))
                    .ForMember(x => x.AuthorId,
                        map => map.MapFrom(
                            dest => dest.Author.Id
                        ))
                    .ForMember(x => x.GenreId,
                        map => map.MapFrom(
                            dest => dest.Genre.Id
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
            }, typeof(Startup));
        }
    }
}