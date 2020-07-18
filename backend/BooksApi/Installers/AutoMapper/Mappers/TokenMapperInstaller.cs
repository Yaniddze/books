using AutoMapper;
using BooksApi.DataBase.Entities;
using BooksApi.Entities;

namespace BooksApi.Installers.AutoMapper.Mappers
{
    public class TokenMapperInstaller: IMapperInstaller
    {
        public void InstallMapper(IMapperConfigurationExpression options)
        {
            options.CreateMap<Token, TokenDB>()
                .ForMember(x => x.Active,
                    map => map.MapFrom(
                        dest => dest.Active
                    ))
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ))
                .ForMember(x => x.CreationDate,
                    map => map.MapFrom(
                        dest => dest.CreationDate
                    ))
                .ForMember(x => x.ExpiryDate,
                    map => map.MapFrom(
                        dest => dest.ExpiryDate
                    ))
                .ForMember(x => x.JwtId,
                    map => map.MapFrom(
                        dest => dest.JwtId
                    ))
                .ForMember(x => x.TokenValue,
                    map => map.MapFrom(
                        dest => dest.TokenValue
                    ))
                .ForMember(x => x.UserId,
                    map => map.MapFrom(
                        dest => dest.UserId
                    ));
            
            options.CreateMap<TokenDB, Token>()
                .ForMember(x => x.Active,
                    map => map.MapFrom(
                        dest => dest.Active
                    ))
                .ForMember(x => x.Id,
                    map => map.MapFrom(
                        dest => dest.Id
                    ))
                .ForMember(x => x.CreationDate,
                    map => map.MapFrom(
                        dest => dest.CreationDate
                    ))
                .ForMember(x => x.ExpiryDate,
                    map => map.MapFrom(
                        dest => dest.ExpiryDate
                    ))
                .ForMember(x => x.JwtId,
                    map => map.MapFrom(
                        dest => dest.JwtId
                    ))
                .ForMember(x => x.TokenValue,
                    map => map.MapFrom(
                        dest => dest.TokenValue
                    ))
                .ForMember(x => x.UserId,
                    map => map.MapFrom(
                        dest => dest.UserId
                    ));
        }
    }
}