using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class ListarCartasHandler : IRequestHandler<ListarCartasQuery, List<DtoCarta>>
    {
        private readonly ApiDbContext context;

        public ListarCartasHandler(ApiDbContext context)
        {
            this.context = context;
        }

        public async Task<List<DtoCarta>> Handle(ListarCartasQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new DtoCarta[] { new DtoCarta { Id = Guid.NewGuid(), Valor = 20 } }.ToList()).ConfigureAwait(false);
        }
    }
}
