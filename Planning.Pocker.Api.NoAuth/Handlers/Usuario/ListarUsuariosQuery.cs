using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class ListarUsuariosQuery : IRequest<List<DtoUsuario>>
    {
        [DataMember]
        public string Nombre { get; set; }
    }
}
