using Microsoft.EntityFrameworkCore;
using WebApp.Data.Models;

namespace WebApp.Data
{
    public class MoodContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(@"Host=localhost;Database=webapp;Username=postgres;Password=levik1234")
         .UseSnakeCaseNamingConvention()
         .UseLoggerFactory(LoggerFactory.Create(builder =>
        builder.AddConsole())).EnableSensitiveDataLogging();
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Publisher>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Author>().HasMany(au => au.Books).WithOne(b => b.Author);
            modelBuilder.Entity<Publisher>().HasMany(p => p.Books).WithOne(b => b.Publisher);
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

    }
}
