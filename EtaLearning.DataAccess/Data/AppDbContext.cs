using EtaLearning.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace EtaLearning.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<DbClient> Clients { get; set; }
        public DbSet<SmartDevice> SmartDevices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbClient>().HasKey(c => c.Id);
            modelBuilder.Entity<SmartDevice>().HasKey(s => s.Id);

            modelBuilder.Entity<SmartDevice>()
                .HasOne(s => s.Client)
                .WithMany(c => c.SmartDevices)
                .HasForeignKey(s => s.ClientId); // Use ClientId as the foreign key
        }


    }
}

