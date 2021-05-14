using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System.Runtime.Serialization;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    [DataContract]
    public class CreateCartaCommand : IRequest<DtoCarta>
    {
        [DataMember]
        public int Valor { get; set; }
    }
}
