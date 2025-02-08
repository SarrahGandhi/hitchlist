using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using HitchList.Data;
using Microsoft.EntityFrameworkCore;
using HitchList.Models;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")] // Base route is "api/EventAPI"
public class EventAPIController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EventAPIController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the list of all events.
    /// </summary>
    /// <returns>A list of all events.</returns>
    [HttpGet("Event")] // Combined route: "api/EventAPI/Event"
    public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
    {
        List<Event> events = await _context.Event.ToListAsync();
        List<Event> eventList = new List<Event>();
        foreach (var eventItem in events)
        {
            eventList.Add(new Event()
            {
                Id = eventItem.Id,
                Name = eventItem.Name,
                Date = eventItem.Date,
                Location = eventItem.Location,
                Category = eventItem.Category
            });
        }
        return Ok(eventList); // Moved outside the loop
    }

    /// <summary>
    /// Finds a specific event by its ID.
    /// </summary>
    /// <param name="id">The ID of the event.</param>
    /// <returns>The event with the specified ID, or a NotFound response if not found.</returns>
    [HttpGet("Event{id}")]
    public async Task<ActionResult<Event>> FindEvent(int id)
    {
        var eventItem = await _context.Event.FindAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        Event events = new Event()
        {
            Id = eventItem.Id,
            Name = eventItem.Name,
            Date = eventItem.Date,
            Location = eventItem.Location,
            Category = eventItem.Category
        };
        return Ok(events);
    }

    /// <summary>
    /// Adds a new event to the database.
    /// </summary>
    /// <param name="addEvent">The event details to be added.</param>
    /// <returns>The newly created event.</returns>
    [HttpPost("AddEvent")]
    public async Task<ActionResult<Event>> AddEvent(EventDto addEvent)
    {
        Event eventItem = new Event()
        {
            Name = addEvent.Name,
            Date = addEvent.Date,
            Location = addEvent.Location,
            Category = addEvent.Category
        };
        _context.Event.Add(eventItem);
        await _context.SaveChangesAsync();

        EventDto eventDto = new EventDto()
        {
            Name = eventItem.Name,
            Date = eventItem.Date.HasValue ? eventItem.Date.Value : DateTime.Now,
            Location = eventItem.Location,
            Category = eventItem.Category
        };

        return CreatedAtAction("FindEvent", new { id = eventItem.Id }, eventDto);
    }

    /// <summary>
    /// Updates an existing event.
    /// </summary>
    /// <param name="id">The ID of the event to update.</param>
    /// <param name="updateEvent">The updated event details.</param>
    /// <returns>A NoContent response if successful, or NotFound if the event doesn't exist.</returns>
    [HttpPut("UpdateEvent/{id}")]
    public async Task<IActionResult> UpdateEvent(int id, Event updateEvent)
    {
        var tasks = await _context.Event.FindAsync(id);
        if (tasks == null)
        {
            return NotFound();
        }

        tasks.Id = id;
        tasks.Name = updateEvent.Name;
        tasks.Date = updateEvent.Date;
        tasks.Location = updateEvent.Location;
        tasks.Category = updateEvent.Category;

        _context.Entry(tasks).State = EntityState.Modified;
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

    /// <summary>
    /// Deletes a specific event by its ID.
    /// </summary>
    /// <param name="id">The ID of the event to delete.</param>
    /// <returns>A NoContent response if successful, or NotFound if the event doesn't exist.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var eventItem = await _context.Event.FindAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }

        _context.Event.Remove(eventItem);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Checks if an event exists in the database.
    /// </summary>
    /// <param name="id">The ID of the event.</param>
    /// <returns>True if the event exists, otherwise false.</returns>
    private bool EventExists(int id)
    {
        return _context.Event.Any(e => e.Id == id);
    }
}
