using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sydenham_Library_System.Models;

namespace Sydenham_Library_System.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<AUTHOR> AUTHOR { get; set; }
    public DbSet<AVAILABILITY> AVAILABILITY { get; set; }
    public DbSet<GENRES> GENRES { get; set; }
    public DbSet<PRODTYPES> PRODUCTTYPES { get; set; }
    public DbSet<PRODUCTS> Products { get; set; }
    public DbSet<MESSAGES> MESSAGES { get; set; }
    public DbSet<RESERVATIONS> RESERVATIONS { get; set; }
    public DbSet<ITEMISSUE> ITEMISSUES { get; set; }
}
