using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class GetVotoQuery : IRequest<DtoVoto>
    {
        public Guid Id { get; }

        public GetVotoQuery(Guid id) => Id = id;
    }
}
