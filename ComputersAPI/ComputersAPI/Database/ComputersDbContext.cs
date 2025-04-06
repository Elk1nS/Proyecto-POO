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
    }
}
