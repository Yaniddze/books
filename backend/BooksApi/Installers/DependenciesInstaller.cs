using BooksApi.UseCases.AddBook;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksApi.Installers
{
    public class DependenciesInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidator<AddBookRequest>, AddBookRequestValidator>();
        }
    }
}