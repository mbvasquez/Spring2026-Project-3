using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spring2026_Project3_mbvasquez.Models;

namespace Spring2026_Project3_mbvasquez.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Spring2026_Project3_mbvasquez.Models.Movie> Movie { get; set; } = default!;

        public DbSet<Spring2026_Project3_mbvasquez.Models.Actor> Actor { get; set; } = default!;
        public DbSet<Spring2026_Project3_mbvasquez.Models.ActorMovie> ActorMovie { get; set; } = default!;

    }
}
