using Microsoft.EntityFrameworkCore;
using RideShare.Models;

namespace RideShare.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt): base(opt)
        {
            
        }

        public DbSet<Travel> Travels { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasKey(tr => new{ tr.TravelId, tr.UserId});

            modelBuilder.Entity<Trip>()
                .HasOne(x => x.User)
                .WithMany(y => y.Trips)
                .HasForeignKey(z => z.UserId);
                
            modelBuilder.Entity<Trip>()
                .HasOne(ab => ab.Travel)
                .WithMany(ac => ac.Trips)
                .HasForeignKey(ad => ad.TravelId);
        }
    }
}