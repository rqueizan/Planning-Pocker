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
    public class VotosController : BaseController
    {
        // GET: api/Votoes
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DtoVoto>))]
        public async Task<ActionResult<IEnumerable<DtoVoto>>> GetVoto([FromQuery] ListarVotosQuery listarVotos)
            => await Mediator.Send(listarVotos).ConfigureAwait(false);

        // GET: api/Votoes/5
        [HttpGet("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoVoto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DtoVoto>> GetVoto(Guid id)
        {
            var voto = await Mediator.Send(new GetVotoQuery(id)).ConfigureAwait(false);
            return voto is null ? NotFound() : voto;
        }

        // PUT: api/Votoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutVoto([FromRoute] Guid validationId, UpdateVotoCommand updateVoto)
            => StatusCode(await Mediator.Send(updateVoto.SetValidationId(validationId)).ConfigureAwait(false));

        // POST: api/Votoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoVoto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DtoVoto>> PostVoto([FromBody] CreateVotoCommand createVoto)
        {
            var dtoVoto = await Mediator.Send(createVoto).ConfigureAwait(false);
            return dtoVoto != null ? CreatedAtAction($"{nameof(VotosController.GetVoto)}", new { id = dtoVoto.Id }, dtoVoto) : BadRequest();
        }

        // DELETE: api/Votoes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVoto([FromRoute] Guid id)
            => StatusCode(await Mediator.Send(new DeleteVotoCommand(id)).ConfigureAwait(false));
    }
}
