using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using HitchList.Data;
using Microsoft.EntityFrameworkCore;
using HitchList.Models;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class GuestController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public GuestController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets the list of all guests.
    /// </summary>
    /// <returns>A list of all guests.</returns>
    [HttpGet("Guest")]
    public async Task<ActionResult<IEnumerable<Guest>>> GetGuest()
    {
        List<Guest> guests = await _context.Guest.ToListAsync();
        List<Guest> guestList = new List<Guest>();
        foreach (var guestItem in guests)
        {
            guestList.Add(new Guest()
            {
                Id = guestItem.Id,
                Name = guestItem.Name,
                InvitationStatus = guestItem.InvitationStatus,
                Category = guestItem.Category
            });
        }
        return Ok(guestList); // Moved outside the loop
    }

    /// <summary>
    /// Finds a specific guest by their ID.
    /// </summary>
    /// <param name="id">The ID of the guest.</param>
    /// <returns>The guest with the specified ID, or a NotFound response if not found.</returns>
    [HttpGet("Guest{id}")]
    public async Task<ActionResult<Guest>> FindGuest(int id)
    {
        var guest = await _context.Guest.FindAsync(id);
        if (guest == null)
        {
            return NotFound();
        }
        Guest guests = new Guest()
        {
            Id = guest.Id,
            Name = guest.Name,
            Category = guest.Category
        };
        return Ok(guests);
    }

    /// <summary>
    /// Adds a new guest to the database.
    /// </summary>
    /// <param name="addguest">The guest details to be added.</param>
    /// <returns>The newly created guest.</returns>
    [HttpPost("AddGuest")]
    public async Task<ActionResult<Guest>> AddGuest(GuestDto addguest)
    {
        Guest guest = new Guest()
        {
            Name = addguest.Name,
            Category = addguest.Category
        };
        _context.Guest.Add(guest);
        await _context.SaveChangesAsync();

        GuestDto guestDto = new GuestDto()
        {
            Id = guest.Id,
            Name = guest.Name,
            Category = guest.Category
        };
        return CreatedAtAction("FindGuest", new { id = guest.Id }, guestDto);
    }

    /// <summary>
    /// Updates an existing guest's information.
    /// </summary>
    /// <param name="id">The ID of the guest to update.</param>
    /// <param name="updateguest">The updated guest details.</param>
    /// <returns>A NoContent response if successful, or NotFound if the guest doesn't exist.</returns>
    [HttpPut("UpdateGuest/{id}")]
    public async Task<IActionResult> UpdateGuest(int id, Guest updateguest)
    {
        var guest = await _context.Guest.FindAsync(id);
        if (guest == null)
        {
            return NotFound();
        }

        guest.Id = id;
        guest.Name = updateguest.Name;
        guest.Category = updateguest.Category;

        _context.Entry(guest).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GuestExists(id))
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
    /// Deletes a specific guest by their ID.
    /// </summary>
    /// <param name="id">The ID of the guest to delete.</param>
    /// <returns>A NoContent response if successful, or NotFound if the guest doesn't exist.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGuest(int id)
    {
        var guest = await _context.Guest.FindAsync(id);
        if (guest == null)
        {
            return NotFound();
        }
        _context.Guest.Remove(guest);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    /// <summary>
    /// Checks if a guest exists in the database.
    /// </summary>
    /// <param name="id">The ID of the guest.</param>
    /// <returns>True if the guest exists, otherwise false.</returns>
    private bool GuestExists(int id)
    {
        return _context.Guest.Any(e => e.Id == id);
    }
}
