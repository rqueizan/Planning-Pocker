using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class CreateHistorialHandler : BaseHandler, IRequestHandler<CreateHistorialCommand, DtoHistorial>
    {
        public CreateHistorialHandler(ApiDbContext context) : base(context) { }

        public async Task<DtoHistorial> Handle(CreateHistorialCommand request, CancellationToken cancellationToken)
        {
            Historial historial;
            Context.Historial.Add(historial = new Historial { Descripcion = request.Descripcion });
            await SaveChangesAsync();
            return AutoMapperHelper.MapDto<Historial, DtoHistorial>(historial);
        }
    }
}
