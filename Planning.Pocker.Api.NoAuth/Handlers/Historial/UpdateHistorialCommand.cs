using MediatR;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class UpdateHistorialCommand : IRequest<int>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        internal Guid ValidationId { get; private set; }

        public UpdateHistorialCommand SetValidationId(Guid validationId)
        {
            ValidationId = validationId;
            return this;
        }
    }
}
