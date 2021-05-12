using System;

namespace Planning.Pocker.Api.NoAuth.Model
{
    public class Voto
    {
        public Guid Id { get; set; }

        public Usuario Usuario { get; set; }

        public Guid UsuarioId { get; set; }

        public Carta Carta { get; set; }

        public Guid CartaId { get; set; }

        public Historial Historial { get; set; }

        public Guid HistorialId { get; set; }
    }
}
