using MediatR;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class UpdateUsuarioCommand : IRequest<int>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        internal Guid ValidationId { get; private set; }

        public UpdateUsuarioCommand SetValidationId(Guid validationId)
        {
            ValidationId = validationId;
            return this;
        }
    }
}
