using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class GetUsuarioHandler : BaseHandler, IRequestHandler<GetUsuarioQuery, DtoUsuario>
    {
        public GetUsuarioHandler(ApiDbContext context) : base(context) { }

        public async Task<DtoUsuario> Handle(GetUsuarioQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(AutoMapperHelper.MapDto<Usuario, DtoUsuario>(Context.Usuario.FirstOrDefault(c => c.Id == request.Id))).ConfigureAwait(false);
        }
    }
}
