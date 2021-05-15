using MediatR;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class UpdateVotoCommand : IRequest<int>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid UsuarioId { get; set; }

        [DataMember]
        public Guid CartaId { get; set; }

        [DataMember]
        public Guid HistorialId { get; set; }

        internal Guid ValidationId { get; private set; }

        public UpdateVotoCommand SetValidationId(Guid validationId)
        {
            ValidationId = validationId;
            return this;
        }
    }
}
