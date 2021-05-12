using AutoMapper;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public static class MapperConfigurationRepository
    {
        public static readonly MapperConfiguration Default = new(cfg =>
        {
            cfg.ShouldMapField = _ => false;
            cfg.CreateMap<Carta, DtoCarta>();
            cfg.CreateMap<Historial, DtoHistorial>();
            cfg.CreateMap<Usuario, DtoUsuario>();
            cfg.CreateMap<Voto, DtoVoto>();
        });
    }
}
