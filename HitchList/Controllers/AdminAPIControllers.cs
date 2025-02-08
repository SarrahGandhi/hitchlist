using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using HitchList.Data;
using Microsoft.EntityFrameworkCore;
using HitchList.Models;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")] // Base route is "api/AdminAPI"
public class AdminAPIController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public AdminAPIController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a list of all administrators.
    /// </summary>
    /// <returns>A list of Admin objects.</returns>
    [HttpGet("Admins")] // Combined route: "api/AdminAPI/Admins"
    public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
    {
        List<Admin> admins = await _context.Admin.ToListAsync();
        List<Admin> adminList = new List<Admin>();
        foreach (var admin in admins)
        {
            adminList.Add(new Admin()
            {
                User_Id = admin.User_Id,
                Username = admin.Username,
                User_phone = admin.User_phone,
                Password_hash = admin.Password_hash,
                Category = admin.Category
            });
        }
        return Ok(adminList);
    }

    /// <summary>
    /// Retrieves a specific administrator by their ID.
    /// </summary>
    /// <param name="id">The ID of the admin to retrieve.</param>
    /// <returns>An Admin object or a NotFound result if not found.</returns>
    [HttpGet("Admin{id}")]
    public async Task<ActionResult<Admin>> FindAdmin(int id)
    {
        var admin = await _context.Admin.FindAsync(id);
        if (admin == null)
        {
            return NotFound();
        }
        Admin admins = new Admin()
        {
            User_Id = admin.User_Id,
            Username = admin.Username,
            User_phone = admin.User_phone,
            Password_hash = admin.Password_hash,
            Category = admin.Category
        };
        return Ok(admins);
    }

    /// <summary>
    /// Adds a new administrator.
    /// </summary>
    /// <param name="addadmin">The admin data to be added.</param>
    /// <returns>A success message along with the newly created admin's ID.</returns>
    [HttpPost("AddAdmin")]
    public async Task<IActionResult> AddAdmin(AdminDto addadmin)
    {
        try
        {
            // Validate the input
            if (string.IsNullOrEmpty(addadmin.Username) || string.IsNullOrEmpty(addadmin.Password_hash))
            {
                return BadRequest(new { message = "Username and Password are required." });
            }
            if (string.IsNullOrEmpty(addadmin.User_phone))
            {
                return BadRequest(new { message = "User phone number is required." });
            }

            // Create new Admin object
            Admin admin = new Admin()
            {
                Username = addadmin.Username,
                User_phone = addadmin.User_phone,
                Password_hash = addadmin.Password_hash,
                Category = addadmin.Category
            };

            // Save to the database
            _context.Admin.Add(admin);
            await _context.SaveChangesAsync();

            // Return success message with Admin ID
            return CreatedAtAction("FindAdmin", new { id = admin.User_Id },
                new { message = $"Admin {admin.User_Id} created successfully.", admin });
        }
        catch (Exception ex)
        {
            // Log the error (Optional)
            Console.WriteLine($"Error: {ex.Message}");

            // Return error message
            return StatusCode(500, new { message = "An error occurred while creating the admin.", error = ex.Message });
        }
    }

    /// <summary>
    /// Updates the details of an existing administrator.
    /// </summary>
    /// <param name="id">The ID of the admin to update.</param>
    /// <param name="updateadmin">The updated admin data.</param>
    /// <returns>A success message if updated successfully or a NotFound result.</returns>
    [HttpPut("UpdateAdmin/{id}")]
    public async Task<IActionResult> UpdateAdmin(int id, Admin updateadmin)
    {
        var admin = await _context.Admin.FindAsync(id);
        if (admin == null)
        {
            return NotFound();
        }

        // Update only the necessary fields
        admin.User_Id = id;
        admin.Username = updateadmin.Username;
        admin.User_phone = updateadmin.User_phone;
        admin.Password_hash = updateadmin.Password_hash;
        admin.Category = updateadmin.Category;

        _context.Entry(admin).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AdminExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return Ok($"Admin {admin.User_Id} updated successfully.");
    }

    /// <summary>
    /// Deletes an administrator by their ID.
    /// </summary>
    /// <param name="id">The ID of the admin to delete.</param>
    /// <returns>A success message if deleted successfully or a NotFound result.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdmin(int id)
    {
        var admin = await _context.Admin.FindAsync(id);
        if (admin == null)
        {
            return NotFound();
        }
        _context.Admin.Remove(admin);
        await _context.SaveChangesAsync();
        return Ok($"Admin {admin.User_Id} deleted successfully.");
    }

    /// <summary>
    /// Checks whether an admin exists by their ID.
    /// </summary>
    /// <param name="id">The ID of the admin.</param>
    /// <returns>True if the admin exists, otherwise false.</returns>
    private bool AdminExists(int id)
    {
        return _context.Admin.Any(e => e.User_Id == id);
    }
}
