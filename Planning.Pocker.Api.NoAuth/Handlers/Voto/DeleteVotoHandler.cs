using MediatR;
using Microsoft.AspNetCore.Http;
using Planning.Pocker.Api.NoAuth.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class DeleteVotoHandler : BaseHandler, IRequestHandler<DeleteVotoCommand, int>
    {
        public DeleteVotoHandler(ApiDbContext context) : base(context) { }

        public async Task<int> Handle(DeleteVotoCommand request, CancellationToken cancellationToken)
        {
            var voto = Context.Voto.FirstOrDefault(c => c.Id == request.Id);
            if (voto is null)
                return StatusCodes.Status404NotFound;
            Context.Voto.Remove(voto);
            await SaveChangesAsync();
            return StatusCodes.Status204NoContent;
        }
    }
}
