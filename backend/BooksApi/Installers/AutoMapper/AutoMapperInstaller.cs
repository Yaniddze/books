using System;
using System.Linq;
using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksApi.Installers.AutoMapper
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(options =>
            {
                options.AddExpressionMapping();

                var mappers = typeof(Startup).Assembly.ExportedTypes
                    .Where(x => typeof(IMapperInstaller).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                    .Select(Activator.CreateInstance)
                    .Cast<IMapperInstaller>()
                    .ToList();
                
                mappers.ForEach(x => x.InstallMapper(options));

            }, typeof(Startup));
        }
    }
}