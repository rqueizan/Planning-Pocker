using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class ListarHistorialesQuery : IRequest<List<DtoHistorial>>
    {
        [DataMember]
        public string Descripcion { get; set; }
    }
}
