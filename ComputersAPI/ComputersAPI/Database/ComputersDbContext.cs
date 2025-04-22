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


        //Metodo para evitar que se eliminen los componentes y perifericos con las computadoras que estan asociados
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ComputerComponentEntity>()
                .HasOne(cc => cc.Component)
                .WithMany()
                .HasForeignKey(cc => cc.ComponentId)
                .OnDelete(DeleteBehavior.Restrict); //  Esto evita el borrado en cascada

            modelBuilder.Entity<ComputerPeripheralEntity>()
                .HasOne(cp => cp.Peripheral)
                .WithMany()
                .HasForeignKey(cp => cp.PeripheralId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
