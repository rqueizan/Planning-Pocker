using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class GetCartaQuery : IRequest<DtoCarta>
    {
        public Guid Id { get; }

        public GetCartaQuery(Guid id) => Id = id;
    }
}
