using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class CreateHistorialCommand : IRequest<DtoHistorial>
    {
        [DataMember]
        public string Descripcion { get; set; }
    }
}
