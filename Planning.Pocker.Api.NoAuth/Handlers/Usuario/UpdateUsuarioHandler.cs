using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class UpdateUsuarioHandler : BaseHandler, IRequestHandler<UpdateUsuarioCommand, int>
    {
        public UpdateUsuarioHandler(ApiDbContext context) : base(context) { }

        public async Task<int> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (request.ValidationId != request.Id)
                return StatusCodes.Status400BadRequest;
            var usuario = Context.Usuario.FirstOrDefault(c => c.Id == request.Id);
            if (usuario is null)
                return StatusCodes.Status404NotFound;
            usuario.Nombre = request.Nombre;
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
