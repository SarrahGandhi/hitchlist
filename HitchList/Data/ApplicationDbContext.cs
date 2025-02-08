using System.Configuration;
using HitchList.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Task = HitchList.Models.Task;

namespace HitchList.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Ensure the 'Id' column is 'VARCHAR(255)' instead of 'TEXT' for Identity tables
        builder.Entity<IdentityRole>(entity =>
        {
            entity.Property(r => r.Id)
                .HasColumnType("varchar(255)");  // Set Id column to VARCHAR(255)
        });

        builder.Entity<IdentityUser>(entity =>
        {
            entity.Property(u => u.Id)
                .HasColumnType("varchar(255)");  // Set Id column to VARCHAR(255)
        });

        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.Property(e => e.UserId)
                .HasColumnType("varchar(255)");  // Set UserId to VARCHAR(255)
            entity.Property(e => e.RoleId)
                .HasColumnType("varchar(255)");  // Set RoleId to VARCHAR(255)
        });
    }


    public DbSet<Admin> Admin { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Task> Task { get; set; }
    public DbSet<Guest> Guest { get; set; }
}

