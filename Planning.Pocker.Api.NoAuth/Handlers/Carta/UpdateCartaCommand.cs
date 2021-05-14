using MediatR;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class UpdateCartaCommand : IRequest<int>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public int Valor { get; set; }

        internal Guid ValidationId { get; private set; }

        public UpdateCartaCommand SetValidationId(Guid validationId)
        {
            ValidationId = validationId;
            return this;
        }
    }
}
