﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Handlers;
using Planning.Pocker.Api.NoAuth.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartasController : ControllerBase
    {
        private readonly IMediator mediator;

        public CartasController(IMediator mediator) => this.mediator = mediator;

        // GET: api/Cartas
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DtoCarta>))]
        public async Task<ActionResult<IEnumerable<DtoCarta>>> GetCarta([FromQuery] ListarCartasQuery listarCartas) => await mediator.Send(listarCartas).ConfigureAwait(false);

        // GET: api/Cartas/5
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoCarta))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DtoCarta>> GetCarta(Guid id)
        {
            var carta = await mediator.Send(new GetCartaQuery(id)).ConfigureAwait(false);
            return carta is null ? NotFound() : carta;
        }

        // PUT: api/Cartas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{validationId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutCarta([FromRoute] Guid validationId, UpdateCartaCommand updateCarta)
            => StatusCode(await mediator.Send(updateCarta.SetValidationId(validationId)).ConfigureAwait(false));

        // POST: api/Cartas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoCarta))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Carta>> PostCarta([FromBody] CreateCartaCommand createCarta)
        {
            var dtoCarta = await mediator.Send(createCarta).ConfigureAwait(false);
            return CreatedAtAction($"{nameof(CartasController.GetCarta)}", new { id = dtoCarta.Id }, dtoCarta);
        }

        // DELETE: api/Cartas/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCarta(DeleteCartaCommand deleteCarta) => StatusCode(await mediator.Send(deleteCarta).ConfigureAwait(false));
    }
}
