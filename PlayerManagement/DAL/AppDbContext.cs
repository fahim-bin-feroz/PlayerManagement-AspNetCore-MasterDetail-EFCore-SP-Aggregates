using Microsoft.EntityFrameworkCore;
using PlayerManagement.Models;

namespace PlayerManagement.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> op):base(op)
        {
            
        }
        public virtual DbSet<Player> Players  { get; set; }
        public virtual DbSet<PlayerSkill> PlayerSkills { get; set; }
        public virtual DbSet<Citizenship> Citizenships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizenship>().HasData(
                new Citizenship { CitizenshipId = 1, CountryName = "Bangladesh" },
                new Citizenship { CitizenshipId = 2, CountryName = "Australia" },
                new Citizenship { CitizenshipId = 3, CountryName = "New Zealand" }
                );
        }
    }
}
