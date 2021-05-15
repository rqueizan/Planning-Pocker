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
    public class ListarHistorialsHandler : BaseHandler, IRequestHandler<ListarHistorialesQuery, List<DtoHistorial>>
    {
        public ListarHistorialsHandler(ApiDbContext context) : base(context) { }

        public async Task<List<DtoHistorial>> Handle(ListarHistorialesQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Historial> historiales = Context.Historial.OrderBy(c => c.Descripcion);
            if (!string.IsNullOrWhiteSpace(request.Descripcion))
                historiales = historiales.Where(c => c.Descripcion.Contains(request.Descripcion));
            return await historiales.ProjectTo<DtoHistorial>(AutoMapperHelper.DefaultConfiguration).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
