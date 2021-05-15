using MediatR;
using Microsoft.AspNetCore.Http;
using Planning.Pocker.Api.NoAuth.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class DeleteHistorialHandler : BaseHandler, IRequestHandler<DeleteHistorialCommand, int>
    {
        public DeleteHistorialHandler(ApiDbContext context) : base(context) { }

        public async Task<int> Handle(DeleteHistorialCommand request, CancellationToken cancellationToken)
        {
            var historial = Context.Historial.FirstOrDefault(c => c.Id == request.Id);
            if (historial is null)
                return StatusCodes.Status404NotFound;
            Context.Historial.Remove(historial);
            await SaveChangesAsync();
            return StatusCodes.Status204NoContent;
        }
    }
}
