using HitchList.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task = HitchList.Models.Task;

namespace HitchList.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Admin> Admin { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Task> Task { get; set; }
}
