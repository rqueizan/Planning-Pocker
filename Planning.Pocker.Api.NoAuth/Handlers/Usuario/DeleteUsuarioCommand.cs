using MediatR;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class DeleteUsuarioCommand : IRequest<int>
    {
        [DataMember]
        public Guid Id { get; private set; }

        public DeleteUsuarioCommand(Guid id) => Id = id;
    }
}
