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
    public class ListarVotosHandler : BaseHandler, IRequestHandler<ListarVotosQuery, List<DtoVoto>>
    {
        public ListarVotosHandler(ApiDbContext context) : base(context) { }

        public async Task<List<DtoVoto>> Handle(ListarVotosQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Voto> votos = Context.Voto
                .Include(v => v.Carta)
                .Include(v => v.Historial)
                .Include(v => v.Usuario)
                .OrderByDescending(c => c.Id);
            if (request.CartaId.HasValue)
                votos = votos.Where(c => c.CartaId == request.CartaId.Value);
            if (request.HistorialId.HasValue)
                votos = votos.Where(c => c.HistorialId == request.HistorialId.Value);
            if (request.UsuarioId.HasValue)
                votos = votos.Where(c => c.UsuarioId == request.UsuarioId.Value);
            if (request.CartaMin != 0)
                votos = votos.Where(c => c.Carta.Valor >= request.CartaMin);
            if (request.CartaMax != 0)
                votos = votos.Where(c => c.Carta.Valor <= request.CartaMax);
            if (!string.IsNullOrWhiteSpace(request.HistorialDescripcion))
                votos = votos.Where(c => request.HistorialDescripcion.Contains(request.HistorialDescripcion));
            if (!string.IsNullOrWhiteSpace(request.UsuarioNombre))
                votos = votos.Where(c => request.HistorialDescripcion.Contains(request.UsuarioNombre));
            return await votos.ProjectTo<DtoVoto>(AutoMapperHelper.DefaultConfiguration).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
