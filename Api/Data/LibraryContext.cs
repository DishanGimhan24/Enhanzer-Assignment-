using Microsoft.EntityFrameworkCore;
using LibraryManagementApi.Models;

namespace LibraryManagementApi.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        // DbSet for books
        public DbSet<Book> Books { get; set; }

        // DbSet for users
        public DbSet<User> Users { get; set; }  // Add this line for users

        // Optional: Override OnModelCreating for custom configurations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Example of configuring entity properties
            modelBuilder.Entity<Book>()
                .HasKey(b => b.Id); // Assuming Book has an Id property

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Assuming User has an Id property
            
            // Add more configurations as needed
        }
    }
}
