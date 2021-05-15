using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class CreateUsuarioHandler : BaseHandler, IRequestHandler<CreateUsuarioCommand, DtoUsuario>
    {
        public CreateUsuarioHandler(ApiDbContext context) : base(context) { }

        public async Task<DtoUsuario> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario usuario;
            Context.Usuario.Add(usuario = new Usuario { Nombre = request.Nombre });
            await SaveChangesAsync();
            return AutoMapperHelper.MapDto<Usuario, DtoUsuario>(usuario);
        }
    }
}
