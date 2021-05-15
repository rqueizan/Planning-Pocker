using MediatR;
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
    public class HistorialController : ControllerBase
    {
        private readonly IMediator mediator;

        public HistorialController(IMediator mediator) => this.mediator = mediator;

        // GET: api/Historial
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DtoHistorial>))]
        public async Task<ActionResult<IEnumerable<DtoHistorial>>> GetHistorial([FromQuery] ListarHistorialesQuery listarHistorial)
            => await mediator.Send(listarHistorial).ConfigureAwait(false);

        // GET: api/Historial/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoHistorial))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DtoHistorial>> GetHistorial(Guid id)
        {
            var historial = await mediator.Send(new GetHistorialQuery(id)).ConfigureAwait(false);
            return historial is null ? NotFound() : historial;
        }

        // PUT: api/Historial/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutHistorialUsuario([FromRoute] Guid validationId, UpdateHistorialCommand updateHistorial)
                => StatusCode(await mediator.Send(updateHistorial.SetValidationId(validationId)).ConfigureAwait(false));

        // POST: api/Historial
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoHistorial))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DtoHistorial>> PostHistorialUsuario([FromBody] CreateHistorialCommand createHistorial)
        {
            var dtoHistorial = await mediator.Send(createHistorial).ConfigureAwait(false);
            return CreatedAtAction($"{nameof(HistorialController.GetHistorial)}", new { id = dtoHistorial.Id }, dtoHistorial);
        }

        // DELETE: api/Historial/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteHistorial([FromRoute] Guid id)
            => StatusCode(await mediator.Send(new DeleteHistorialCommand(id)).ConfigureAwait(false));
    }
}
