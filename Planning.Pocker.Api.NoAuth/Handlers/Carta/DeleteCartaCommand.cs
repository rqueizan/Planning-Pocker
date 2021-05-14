using MediatR;
using System;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class DeleteCartaCommand : IRequest<int>
    {
        [DataMember]
        public Guid Id { get; set; }
    }
}
