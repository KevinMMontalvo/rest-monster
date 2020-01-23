using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConcertShowRestServer.Model;
using Newtonsoft.Json.Linq;

namespace ConcertShowRestServer.Controllers
{
    [Route("rest/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: rest/Tickets
        [HttpGet]
        public IEnumerable<Ticket> GetTicket()
        {
            return _context.Ticket;
        }

        // GET: rest/Tickets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Ticket.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        // GET: rest/Tickets/available/5
        [HttpGet("available/{spectacleId}")]
        public async Task<IActionResult> GetAvailableTickets([FromRoute] long spectacleId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Ticket.Include(t => t.Location)
                .Where(t => t.SpectacleId == spectacleId && t.Availability > 0).ToListAsync();

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        // PUT: rest/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket([FromRoute] long id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: rest/Tickets
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new {id = ticket.Id}, ticket);
        }

        // DELETE: rest/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        private bool TicketExists(long id)
        {
            return _context.Ticket.Any(e => e.Id == id);
        }

        // POST: rest/Tickets/updateAvailability/1
        [HttpPost("updateAvailability/{ticketId}")]
        public async Task<IActionResult> UpdateTicketAvailability([FromRoute] long ticketId, [FromBody] JObject body)
        {
            int soldTickets = body.Value<int>("soldTickets");
            Ticket ticket = _context.Ticket.Single(t => t.Id == ticketId);

            if (!ModelState.IsValid || ticket.Availability < soldTickets)
            {
                return BadRequest(ModelState);
            }

            ticket.Availability -= soldTickets;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}