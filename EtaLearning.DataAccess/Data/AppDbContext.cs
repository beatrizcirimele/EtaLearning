using EtaLearning.API.Data.Entities;
using EtaLearning.DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EtaLearning.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {        }
        public DbSet<Client> Clients { get; set; }

        public DbSet<SmartDevice> SmartDevices { get; set; }
    }
}
