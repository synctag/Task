using Microsoft.EntityFrameworkCore;
using TestApplication.Model;

namespace TestApplication.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<CarDetails> CarDetails { get; set; }   
         public MyDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CarDetails>();
        }
    }
}

