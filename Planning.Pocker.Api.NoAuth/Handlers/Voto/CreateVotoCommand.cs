using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class CreateVotoCommand : IRequest<DtoVoto>
    {
        [DataMember]
        public Guid UsuarioId { get; set; }

        [DataMember]
        public Guid CartaId { get; set; }

        [DataMember]
        public Guid HistorialId { get; set; }
    }
}
