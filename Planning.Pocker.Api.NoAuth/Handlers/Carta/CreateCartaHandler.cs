using MediatR;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class CreateCartaHandler : BaseHandler, IRequestHandler<CreateCartaCommand, DtoCarta>
    {
        public CreateCartaHandler(ApiDbContext context) : base(context) { }

        public async Task<DtoCarta> Handle(CreateCartaCommand request, CancellationToken cancellationToken)
        {
            Carta carta;
            Context.Carta.Add(carta = new Carta { Valor = request.Valor });
            await SaveChangesAsync();
            return AutoMapperHelper.MapDto<Carta, DtoCarta>(carta);
        }
    }
}
