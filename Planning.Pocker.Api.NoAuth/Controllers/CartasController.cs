using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Dtos;
using Planning.Pocker.Api.NoAuth.Handlers;
using Planning.Pocker.Api.NoAuth.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Planning.Pocker.Api.NoAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartasController : ControllerBase
    {
        private readonly ApiDbContext context;
        private readonly IMediator mediator;

        public CartasController(IMediator mediator, ApiDbContext context)
        {
            this.mediator = mediator;
            this.context = context;
        }

        // GET: api/Cartas
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<DtoCarta>))]
        public async Task<ActionResult<IEnumerable<DtoCarta>>> GetCarta() => await mediator.Send(new ListarCartasQuery()).ConfigureAwait(false);

        // GET: api/Cartas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carta>> GetCarta(Guid id)
        {
            var carta = await context.Carta.FindAsync(id);

            if (carta == null)
            {
                return NotFound();
            }

            return carta;
        }

        // PUT: api/Cartas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarta(Guid id, Carta carta)
        {
            if (id != carta.Id)
            {
                return BadRequest();
            }

            context.Entry(carta).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cartas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carta>> PostCarta(Carta carta)
        {
            context.Carta.Add(carta);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCarta", new { id = carta.Id }, carta);
        }

        // DELETE: api/Cartas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarta(Guid id)
        {
            var carta = await context.Carta.FindAsync(id);
            if (carta == null)
            {
                return NotFound();
            }

            context.Carta.Remove(carta);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartaExists(Guid id)
        {
            return context.Carta.Any(e => e.Id == id);
        }
    }
}
