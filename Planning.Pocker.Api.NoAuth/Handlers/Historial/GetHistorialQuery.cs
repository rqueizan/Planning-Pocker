using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class GetHistorialQuery : IRequest<DtoHistorial>
    {
        public Guid Id { get; }

        public GetHistorialQuery(Guid id) => Id = id;
    }
}
