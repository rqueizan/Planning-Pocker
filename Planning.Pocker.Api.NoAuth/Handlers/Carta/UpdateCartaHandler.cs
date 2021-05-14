using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class UpdateCartaHandler : BaseHandler, IRequestHandler<UpdateCartaCommand, int>
    {
        public UpdateCartaHandler(ApiDbContext context) : base(context) { }

        public async Task<int> Handle(UpdateCartaCommand request, CancellationToken cancellationToken)
        {
            if (request.ValidationId != request.Id)
                return StatusCodes.Status400BadRequest;
            var carta = Context.Carta.FirstOrDefault(c => c.Id == request.Id);
            if (carta is null)
                return StatusCodes.Status404NotFound;
            carta.Valor = request.Valor;
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
