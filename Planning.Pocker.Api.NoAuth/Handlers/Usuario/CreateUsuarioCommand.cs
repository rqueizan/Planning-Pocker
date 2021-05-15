using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class CreateUsuarioCommand : IRequest<DtoUsuario>
    {
        [DataMember]
        public string Nombre { get; set; }
    }
}
