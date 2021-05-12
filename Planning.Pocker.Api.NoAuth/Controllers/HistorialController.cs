using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Planning.Pocker.Api.NoAuth;
using Planning.Pocker.Api.NoAuth.Data;
using Planning.Pocker.Api.NoAuth.Model;

namespace Planning.Pocker.Api.NoAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public HistorialController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/HistorialUsuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Historial>>> GetHistorialUsuario()
        {
            return await _context.Historial.ToListAsync();
        }

        // GET: api/HistorialUsuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Historial>> GetHistorialUsuario(Guid id)
        {
            var historialUsuario = await _context.Historial.FindAsync(id);

            if (historialUsuario == null)
            {
                return NotFound();
            }

            return historialUsuario;
        }

        // PUT: api/HistorialUsuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistorialUsuario(Guid id, Historial historialUsuario)
        {
            if (id != historialUsuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(historialUsuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistorialUsuarioExists(id))
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

        // POST: api/HistorialUsuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Historial>> PostHistorialUsuario(Historial historialUsuario)
        {
            _context.Historial.Add(historialUsuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistorialUsuario", new { id = historialUsuario.Id }, historialUsuario);
        }

        // DELETE: api/HistorialUsuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorialUsuario(Guid id)
        {
            var historialUsuario = await _context.Historial.FindAsync(id);
            if (historialUsuario == null)
            {
                return NotFound();
            }

            _context.Historial.Remove(historialUsuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistorialUsuarioExists(Guid id)
        {
            return _context.Historial.Any(e => e.Id == id);
        }
    }
}
