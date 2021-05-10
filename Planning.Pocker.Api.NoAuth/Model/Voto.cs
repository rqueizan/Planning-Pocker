using System;

namespace Planning.Pocker.Api.NoAuth
{
    public class Voto
    {
        public Guid Id { get; set; }

        public Usuario Usuario { get; set; }

        public Guid UsuarioId { get; set; }

        public Carta Carta { get; set; }

        public Guid TarjetaId { get; set; }

        public HistorialUsuario HistorialUsuario { get; set; }

        public Guid HistorialUsuarioId { get; set; }
    }
}
