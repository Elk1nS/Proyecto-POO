using ComputersAPI.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComputersAPI.Database
{
    public class ComputersDbContext : DbContext
    {
        public ComputersDbContext(DbContextOptions options) : base(options) 
        { 
        }

        public DbSet<ComputerEntity> Computers { get; set; }
        public DbSet<ComponentEntity> Components { get; set; }
        public DbSet<CategoryComponentEntity> CategoriesComponents { get; set; }
        public DbSet<PeripheralEntity> Peripherals { get; set; }
        public DbSet<CategoryPeripheralEntity> CategoriesPeripherals { get; set; }
        public DbSet<ComputerComponentEntity> ComputerComponents { get; set; }
        public DbSet<ComputerPeripheralEntity> ComputerPeripherals { get; set; }

       

    }
}
