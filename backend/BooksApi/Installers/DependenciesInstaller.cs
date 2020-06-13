using BooksApi.CQRS.Commands;
using BooksApi.CQRS.Commands.Abstractions;
using BooksApi.CQRS.Queries;
using BooksApi.DataBase.Context;
using BooksApi.DataBase.CQRS.AuthorImpl;
using BooksApi.DataBase.CQRS.BookImpl;
using BooksApi.DataBase.CQRS.GenreImpl;
using BooksApi.Entities;
using BooksApi.UseCases.AddBook;
using BooksApi.UseCases.DeleteBooks;
using BooksApi.UseCases.UpdateBook;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksApi.Installers
{
    public class DependenciesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Validators
            services.AddTransient<IValidator<AddBookRequest>, AddBookRequestValidator>();
            services.AddTransient<IValidator<DeleteBooksRequest>, DeleteBookRequestValidator>();
            services.AddTransient<IValidator<UpdateBookRequest>, UpdateBookRequestValidator>();
            
            // Entity context provider
            services.AddSingleton(new ContextProvider());
            
            // CQRS
            services.AddTransient<IFindQuery<Author>, FindAuthorQuery>();
            services.AddTransient<IFindQuery<Genre>, FindGenreQuery>();
            services.AddTransient<IFindQuery<Book>, FindBookQuery>();
            services.AddTransient<IGetAllQuery<Book>, GetAllBooks>();
            services.AddTransient<IGetAllQuery<Author>, GetAllAuthors>();
            services.AddTransient<ICommandHandler<AddBookCommand>, AddBookCommandHandler>();
            services.AddTransient<ICommandHandler<DeleteBooksCommand>, DeleteBookCommandHandler>();
            services.AddTransient<ICommandHandler<UpdateBookCommand>, UpdateBookCommandHandler>();
        }
    }
}