using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcertShowRestServer.Model;

namespace ConcertShowRestServer.Controllers
{
    [Route("rest/[controller]")]
    [ApiController]
    public class SpectaclesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SpectaclesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: rest/Spectacles
        [HttpGet]
        public IEnumerable<Spectacle> GetSpectacle()
        {
            return _context.Spectacle.Include(s => s.Place)
                .Include(s => s.Category)
                .Where(s => s.Date > DateTime.Now).ToList();
        }

        // GET: rest/Spectacles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpectacle([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spectacle = await _context.Spectacle.FindAsync(id);

            if (spectacle == null)
            {
                return NotFound();
            }

            return Ok(spectacle);
        }

        // PUT: rest/Spectacles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpectacle([FromRoute] long id, [FromBody] Spectacle spectacle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != spectacle.Id)
            {
                return BadRequest();
            }

            _context.Entry(spectacle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpectacleExists(id))
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

        // POST: rest/Spectacles
        [HttpPost]
        public async Task<IActionResult> PostSpectacle([FromBody] Spectacle spectacle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Spectacle.Add(spectacle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpectacle", new { id = spectacle.Id }, spectacle);
        }

        // DELETE: rest/Spectacles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpectacle([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var spectacle = await _context.Spectacle.FindAsync(id);
            if (spectacle == null)
            {
                return NotFound();
            }

            _context.Spectacle.Remove(spectacle);
            await _context.SaveChangesAsync();

            return Ok(spectacle);
        }

        private bool SpectacleExists(long id)
        {
            return _context.Spectacle.Any(e => e.Id == id);
        }
    }
}