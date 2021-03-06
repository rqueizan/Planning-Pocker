using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class ListarCartasQuery : IRequest<List<DtoCarta>>
    {
        [DataMember]
        public int Min { get; set; }

        [DataMember]
        public int Max { get; set; }
    }
}
