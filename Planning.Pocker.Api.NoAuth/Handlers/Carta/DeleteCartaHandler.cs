using MediatR;
using Microsoft.AspNetCore.Http;
using Planning.Pocker.Api.NoAuth.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class DeleteCartaHandler : BaseHandler, IRequestHandler<DeleteCartaCommand, int>
    {
        public DeleteCartaHandler(ApiDbContext context) : base(context) { }

        public async Task<int> Handle(DeleteCartaCommand request, CancellationToken cancellationToken)
        {
            var carta = Context.Carta.FirstOrDefault(c => c.Id == request.Id);
            if (carta is null)
                return StatusCodes.Status404NotFound;
            Context.Carta.Remove(carta);
            await SaveChangesAsync();
            return StatusCodes.Status204NoContent;
        }
    }
}
