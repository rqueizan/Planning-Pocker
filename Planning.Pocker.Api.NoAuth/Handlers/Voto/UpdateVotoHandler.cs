using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class UpdateVotoHandler : BaseHandler, IRequestHandler<UpdateVotoCommand, int>
    {
        public UpdateVotoHandler(ApiDbContext context) : base(context) { }

        public async Task<int> Handle(UpdateVotoCommand request, CancellationToken cancellationToken)
        {
            if (request.ValidationId != request.Id)
                return StatusCodes.Status400BadRequest;
            var voto = Context.Voto.FirstOrDefault(c => c.Id == request.Id);
            var carta = Context.Carta.Any(c => c.Id == request.CartaId);
            var historial = Context.Historial.Any(c => c.Id == request.CartaId);
            var usuario = Context.Usuario.Any(c => c.Id == request.CartaId);
            if (!(voto != null && carta && historial && usuario))
                return StatusCodes.Status404NotFound;
            voto.CartaId = request.CartaId;
            voto.HistorialId = request.HistorialId;
            voto.UsuarioId = request.UsuarioId;
            try
            {
                await SaveChangesAsync();
                return StatusCodes.Status204NoContent;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
