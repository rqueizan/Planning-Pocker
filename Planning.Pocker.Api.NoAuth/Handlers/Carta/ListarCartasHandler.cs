using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class ListarCartasHandler : IRequestHandler<ListarCartasQuery, List<DtoCarta>>
    {
        private readonly ApiDbContext context;

        public ListarCartasHandler(ApiDbContext context) => this.context = context;

        public async Task<List<DtoCarta>> Handle(ListarCartasQuery request, CancellationToken cancellationToken)
            => await context.Carta.OrderByDescending(c => c.Valor).ProjectTo<DtoCarta>(MapperConfigurationRepository.Default).ToListAsync().ConfigureAwait(false);
    }
}
