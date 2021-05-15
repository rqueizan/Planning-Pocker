using System;

namespace Planning.Pocker.Api.NoAuth.Dtos
{
    public class DtoVoto
    {
        public Guid Id { get; set; }

        public DtoUsuario Usuario { get; set; }

        public DtoCarta Carta { get; set; }

        public DtoHistorial Historial { get; set; }
    }
}
