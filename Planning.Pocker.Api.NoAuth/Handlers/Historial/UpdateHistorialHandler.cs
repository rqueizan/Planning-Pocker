using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class UpdateHistorialHandler : BaseHandler, IRequestHandler<UpdateHistorialCommand, int>
    {
        public UpdateHistorialHandler(ApiDbContext context) : base(context) { }

        public async Task<int> Handle(UpdateHistorialCommand request, CancellationToken cancellationToken)
        {
            if (request.ValidationId != request.Id)
                return StatusCodes.Status400BadRequest;
            var historial = Context.Historial.FirstOrDefault(c => c.Id == request.Id);
            if (historial is null)
                return StatusCodes.Status404NotFound;
            historial.Descripcion = request.Descripcion;
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
