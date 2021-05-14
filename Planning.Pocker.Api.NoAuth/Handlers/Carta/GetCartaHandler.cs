using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class GetCartaHandler : BaseHandler, IRequestHandler<GetCartaQuery, DtoCarta>
    {
        public GetCartaHandler(ApiDbContext context) : base(context) { }

        public async Task<DtoCarta> Handle(GetCartaQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(AutoMapperHelper.MapDto<Carta, DtoCarta>(Context.Carta.FirstOrDefault(c => c.Id == request.Id))).ConfigureAwait(false);
        }
    }
}
