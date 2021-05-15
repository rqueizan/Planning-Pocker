using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class GetHistorialHandler : BaseHandler, IRequestHandler<GetHistorialQuery, DtoHistorial>
    {
        public GetHistorialHandler(ApiDbContext context) : base(context) { }

        public async Task<DtoHistorial> Handle(GetHistorialQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(AutoMapperHelper.MapDto<Historial, DtoHistorial>(Context.Historial.FirstOrDefault(c => c.Id == request.Id))).ConfigureAwait(false);
        }
    }
}
