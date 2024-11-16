using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Ticketing.Data;
using Ticketing.Dtos.Event;
using Ticketing.Helper;
using Ticketing.Models;

namespace Ticketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public EventsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            return await _context.Events.ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(PostDto post)
        {
            var userIdStr = User.FindFirstValue(JwtRegisteredClaimNames.Sub);

            if (userIdStr == null)
                return Unauthorized(new ApiResponse<object>
                {
                    Code = 401,
                    Status = "error",
                    Data = null,
                    Message = "invalid token"
                });

            var user = await _context.Users.FindAsync(Guid.Parse(userIdStr));

            if (user == null || user.Role == 0)
                return Unauthorized(new ApiResponse<object>
                {
                    Code = 200,
                    Status = "error",
                    Data = null,
                    Message = "unauthorized user"
                });

            if (!DateTime.TryParse(post.Time, out var parsedTime))
                return BadRequest(new ApiResponse<object>
                {
                    Code = 400,
                    Status = "error",
                    Data = null,
                    Message = "invalid time format"
                });

            var newEvent = new Event
            {
                Name = post.Name,
                Description = post.Description,
                time = parsedTime,
                Place = post.Place,
                Price = post.Price,
                Max_Audience = post.Max_Audience,
                Note = post.Note,
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEvent), new { id = newEvent.Id },
                new ApiResponse<object>
                {
                    Code = 201,
                    Status = "success",
                    Data = newEvent,
                    Message = "event successfully created"
                });
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(Guid id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
