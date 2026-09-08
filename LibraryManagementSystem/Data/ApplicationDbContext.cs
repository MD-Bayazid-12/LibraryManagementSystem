using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    BookId = 1,
                    Title = "C# Programming",
                    Author = "John Doe",
                    ISBN = "1234567890",
                    Genre = "Programming",
                    TotalCopies = 5,
                    AvailableCopies = 5,
                    AddedDate = DateTime.Now,
                    Description = "A comprehensive guide to C# programming",
                    BookImage = ""
                },
                new Book
                {
                    BookId = 2,
                    Title = "ASP.NET Core",
                    Author = "Jane Smith",
                    ISBN = "0987654321",
                    Genre = "Programming",
                    TotalCopies = 3,
                    AvailableCopies = 3,
                    AddedDate = DateTime.Now,
                    Description = "Building web applications with ASP.NET Core",
                    BookImage = ""
                }
            );
        }
    }
}