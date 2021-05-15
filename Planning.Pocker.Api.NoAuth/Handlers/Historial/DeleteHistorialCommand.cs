using MediatR;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class DeleteHistorialCommand : IRequest<int>
    {
        [DataMember]
        public Guid Id { get; private set; }

        public DeleteHistorialCommand(Guid id) => Id = id;
    }
}
