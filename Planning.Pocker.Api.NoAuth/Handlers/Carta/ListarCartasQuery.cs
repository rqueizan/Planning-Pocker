using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System.Collections.Generic;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class ListarCartasQuery : IRequest<List<DtoCarta>> { }
}
