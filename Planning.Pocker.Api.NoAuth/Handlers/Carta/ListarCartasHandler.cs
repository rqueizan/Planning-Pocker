using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class ListarCartasHandler : BaseHandler, IRequestHandler<ListarCartasQuery, List<DtoCarta>>
    {
        public ListarCartasHandler(ApiDbContext context) : base(context) { }

        public async Task<List<DtoCarta>> Handle(ListarCartasQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Carta> cartas = Context.Carta.OrderByDescending(c => c.Valor);
            if (request.Min != 0)
                cartas = cartas.Where(c => c.Valor >= request.Min);
            if (request.Max != 0)
                cartas = cartas.Where(c => c.Valor <= request.Max);
            return await cartas.ProjectTo<DtoCarta>(AutoMapperHelper.DefaultConfiguration).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
