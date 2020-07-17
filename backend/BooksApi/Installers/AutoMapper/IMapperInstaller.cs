using AutoMapper;

namespace BooksApi.Installers.AutoMapper
{
    public interface IMapperInstaller
    {
        void InstallMapper(IMapperConfigurationExpression options);
    }
}