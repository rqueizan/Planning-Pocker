using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Handlers;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : BaseController
    {
        // GET: api/Usuarios
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DtoUsuario>))]
        public async Task<ActionResult<IEnumerable<DtoUsuario>>> GetUsuario([FromQuery] ListarUsuariosQuery listarUsuario)
            => await Mediator.Send(listarUsuario).ConfigureAwait(false);

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoUsuario))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DtoUsuario>> GetUsuario(Guid id)
        {
            var usuario = await Mediator.Send(new GetUsuarioQuery(id)).ConfigureAwait(false);
            return usuario is null ? NotFound() : usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutUsuario([FromRoute] Guid validationId, UpdateUsuarioCommand updateUsuario)
            => StatusCode(await Mediator.Send(updateUsuario.SetValidationId(validationId)).ConfigureAwait(false));

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoUsuario))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DtoUsuario>> PostUsuario([FromBody] CreateUsuarioCommand createUsuario)
        {
            var dtoUsuario = await Mediator.Send(createUsuario).ConfigureAwait(false);
            return CreatedAtAction($"{nameof(UsuariosController.GetUsuario)}", new { id = dtoUsuario.Id }, dtoUsuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUsuario([FromRoute] Guid id)
            => StatusCode(await Mediator.Send(new DeleteUsuarioCommand(id)).ConfigureAwait(false));
    }
}
