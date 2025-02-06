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
                // Corrected property name from AdminName to AdminUsername
            });
        }
        return Ok(adminList); // Moved outside the loop
    }
    [HttpGet(template: "Admin{id}")]
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

            // Corrected property name from AdminName to AdminUsername
        };
        return Ok(admins);
    }
    [HttpPost(template: "AddAdmin")]
    public async Task<ActionResult<Admin>> AddAdmin(AdminDto addadmin)
    {
        Admin admin = new Admin()
        {
            Username = addadmin.Username,
            User_phone = addadmin.User_phone,
            Password_hash = addadmin.Password_hash,
            Category = addadmin.Category
        };
        _context.Admin.Add(admin);
        await _context.SaveChangesAsync();
        AdminDto adminDto = new AdminDto()
        {
            User_Id = admin.User_Id,
            Username = admin.Username,
            User_phone = admin.User_phone,
            Password_hash = admin.Password_hash,
            Category = admin.Category
        };
        return CreatedAtAction("FindAdmin", new { id = admin.User_Id }, adminDto);
    }
    [HttpPut("UpdateAdmin/{id}")]
    public async Task<IActionResult> UpdateAdmin(int id, Admin updateadmin)
    {
        // if (id != updateadmin.User_Id)
        // {
        //     return BadRequest();
        // }
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
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAdmin(int id)
    {
        var admin = await _context.Admin.FindAsync(id);
        if (admin == null)
        {
            return NotFound();
            // return NotFound();
        }
        _context.Admin.Remove(admin);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool AdminExists(int id)
    {
        return _context.Admin.Any(e => e.User_Id == id);
    }
}
