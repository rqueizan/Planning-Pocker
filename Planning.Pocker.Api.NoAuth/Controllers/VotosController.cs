using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Handlers;
using Planning.Pocker.Api.NoAuth.Model;

namespace Planning.Pocker.Api.NoAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotosController : ControllerBase
    {
        private readonly IMediator mediator;

        public VotosController(IMediator mediator) => this.mediator = mediator;

        // GET: api/Votoes
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DtoVoto>))]
        public async Task<ActionResult<IEnumerable<DtoVoto>>> GetVoto([FromQuery] ListarVotosQuery listarVotos) 
            => await mediator.Send(listarVotos).ConfigureAwait(false);

        // GET: api/Votoes/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoVoto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DtoVoto>> GetVoto(Guid id)
        {
            var voto = await mediator.Send(new GetVotoQuery(id)).ConfigureAwait(false);
            return voto is null ? NotFound() : voto;
        }

        // PUT: api/Votoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutVoto([FromRoute] Guid validationId, UpdateVotoCommand updateVoto)
            => StatusCode(await mediator.Send(updateVoto.SetValidationId(validationId)).ConfigureAwait(false));

        // POST: api/Votoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoVoto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DtoVoto>> PostVoto([FromBody] CreateVotoCommand createVoto)
        {
            var dtoVoto = await mediator.Send(createVoto).ConfigureAwait(false);
            return dtoVoto != null ? CreatedAtAction($"{nameof(VotosController.GetVoto)}", new { id = dtoVoto.Id }, dtoVoto) : BadRequest();
        }

        // DELETE: api/Votoes/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVoto([FromRoute] Guid id) 
            => StatusCode(await mediator.Send(new DeleteVotoCommand(id)).ConfigureAwait(false));
    }
}
