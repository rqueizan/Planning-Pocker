using MediatR;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class DeleteVotoCommand : IRequest<int>
    {
        [DataMember]
        public Guid Id { get; private set; }

        public DeleteVotoCommand(Guid id) => Id = id;
    }
}
