using AlphaStellarWebApi.Entity;
using Microsoft.EntityFrameworkCore;

namespace AlphaStellarWebApi.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

    
        }

        
        public DbSet<Cars> Cars { get; set; }
        public DbSet<Buses> Busses { get; set; }        
        public DbSet<Boats> Boats { get; set; }


    }
    
}
