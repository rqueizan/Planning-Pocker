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
    public class ListarUsuariosHandler : BaseHandler, IRequestHandler<ListarUsuariosQuery, List<DtoUsuario>>
    {
        public ListarUsuariosHandler(ApiDbContext context) : base(context) { }

        public async Task<List<DtoUsuario>> Handle(ListarUsuariosQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Usuario> usuarios = Context.Usuario.OrderBy(c => c.Nombre);
            if (!string.IsNullOrWhiteSpace(request.Nombre))
                usuarios = usuarios.Where(c => c.Nombre.Contains(request.Nombre));
            return await usuarios.ProjectTo<DtoUsuario>(AutoMapperHelper.DefaultConfiguration).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
