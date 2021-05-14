using AutoMapper;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Diagnostics.CodeAnalysis;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public static class AutoMapperHelper
    {
        public static readonly MapperConfiguration DefaultConfiguration = new(cfg =>
        {
            cfg.ShouldMapField = _ => false;
            cfg.CreateMap<Carta, DtoCarta>();
            cfg.CreateMap<Historial, DtoHistorial>();
            cfg.CreateMap<Usuario, DtoUsuario>();
            cfg.CreateMap<Voto, DtoVoto>();
        });

        public static readonly MapperConfiguration RequestConfiguration = new(cfg =>
        {
            cfg.ShouldMapField = _ => false;
            cfg.CreateMap<CreateCartaCommand, Carta>();
            cfg.CreateMap<DeleteCartaCommand, Carta>();
        });

        private static Mapper SingleMapper => new(DefaultConfiguration);

        private static Mapper RequestMapper => new(RequestConfiguration);

        public static TDestination MapDto<TSource, TDestination>([AllowNull] TSource @object)
        {
            if (@object is null)
                return default;
            return SingleMapper.Map<TSource, TDestination>(@object);
        }

        public static TDestination MapRequest<TSource, TDestination>([AllowNull] TSource @object)
        {
            if (@object is null)
                return default;
            return RequestMapper.Map<TSource, TDestination>(@object);
        }
    }
}
