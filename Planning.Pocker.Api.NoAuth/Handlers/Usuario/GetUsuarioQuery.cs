using MediatR;
using Planning.Pocker.Api.NoAuth.Dtos;
using System;

namespace Planning.Pocker.Api.NoAuth.Handlers
{
    public class GetUsuarioQuery : IRequest<DtoUsuario>
    {
        public Guid Id { get; }

        public GetUsuarioQuery(Guid id) => Id = id;
    }
}
