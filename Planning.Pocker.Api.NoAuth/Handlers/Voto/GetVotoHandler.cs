using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class GetVotoHandler : BaseHandler, IRequestHandler<GetVotoQuery, DtoVoto>
    {
        public GetVotoHandler(ApiDbContext context) : base(context) { }

        public async Task<DtoVoto> Handle(GetVotoQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(AutoMapperHelper.MapDto<Voto, DtoVoto>(Context.Voto.FirstOrDefault(c => c.Id == request.Id))).ConfigureAwait(false);
        }
    }
}
