using MediatR;
using Microsoft.AspNetCore.Http;
using Planning.Pocker.Api.NoAuth.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class DeleteUsuarioHandler : BaseHandler, IRequestHandler<DeleteUsuarioCommand, int>
    {
        public DeleteUsuarioHandler(ApiDbContext context) : base(context) { }

        public async Task<int> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = Context.Usuario.FirstOrDefault(c => c.Id == request.Id);
            if (usuario is null)
                return StatusCodes.Status404NotFound;
            Context.Usuario.Remove(usuario);
            await SaveChangesAsync();
            return StatusCodes.Status204NoContent;
        }
    }
}
