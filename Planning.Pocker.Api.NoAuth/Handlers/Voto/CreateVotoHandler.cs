using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class CreateVotoHandler : BaseHandler, IRequestHandler<CreateVotoCommand, DtoVoto>
    {
        public CreateVotoHandler(ApiDbContext context) : base(context) { }

        public async Task<DtoVoto> Handle(CreateVotoCommand request, CancellationToken cancellationToken)
        {
            var carta = Context.Carta.Any(c => c.Id == request.CartaId);
            var historial = Context.Historial.Any(c => c.Id == request.CartaId);
            var usuario = Context.Usuario.Any(c => c.Id == request.CartaId);
            if (!(carta && historial && usuario))
                return null;
            Voto voto;
            Context.Voto.Add(voto = new Voto { CartaId = request.CartaId, HistorialId = request.HistorialId, UsuarioId = request.UsuarioId });
            await SaveChangesAsync();
            return AutoMapperHelper.MapDto<Voto, DtoVoto>(voto);
        }
    }
}
