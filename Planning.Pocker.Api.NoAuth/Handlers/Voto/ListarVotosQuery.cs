using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class ListarVotosQuery : IRequest<List<DtoVoto>>
    {
        [DataMember]
        public Guid? UsuarioId { get; set; }

        [DataMember]
        public Guid? CartaId { get; set; }

        [DataMember]
        public Guid? HistorialId { get; set; }

        [DataMember]
        public string UsuarioNombre { get; set; }

        [DataMember]
        public int CartaMin { get; set; }

        [DataMember]
        public int CartaMax { get; set; }

        [DataMember]
        public string HistorialDescripcion { get; set; }
    }
}
