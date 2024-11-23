using Expense_Tracker.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.API.Data
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public ExpenseTrackerDbContext(DbContextOptions<ExpenseTrackerDbContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Expense>()
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)"); // Adjust precision and scale as needed

            // One-to-Many: User -> Expenses
            modelBuilder.Entity<User>()
                .HasMany(u => u.Expenses)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.NoAction);  // If a user is deleted, their expenses are also deleted

            // One-to-Many: Category -> Expenses
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Expenses)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);  // If a category is deleted, expenses are set to null

            // One-to-Many: User -> Categories (optional)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Categories)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Seeding Users
            modelBuilder.Entity<User>().HasData(
                new User { UserId = Guid.Parse("b3c5814b-9434-4520-9887-64d1979db9b0"), Username = "zyzornatti", Email = "zyzornatti@etracker.com", PasswordHash = "Oluwatobi123" },
                new User { UserId = Guid.Parse("5a5f8530-846c-4398-a264-1e6cabdd053e"), Username = "zaltego", Email = "zaltego@etracker.com", PasswordHash = "Oluwatobi123" }
            );

            //// Seeding Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = Guid.Parse("4df523f6-5e73-43af-aaf1-0c980e16fc9f"), Name = "Food", UserId = Guid.Parse("b3c5814b-9434-4520-9887-64d1979db9b0") },
                new Category { CategoryId = Guid.Parse("67ecebc9-e619-4f84-97a4-f33f5b59790a"), Name = "Transport", UserId = Guid.Parse("b3c5814b-9434-4520-9887-64d1979db9b0") },
                new Category { CategoryId = Guid.Parse("9d86390b-8e1b-4b82-baff-c0b953f1ef1e"), Name = "Utilities", UserId = Guid.Parse("5a5f8530-846c-4398-a264-1e6cabdd053e") }
            );

            //// Seeding Expenses
            modelBuilder.Entity<Expense>().HasData(
                new Expense { ExpenseId = Guid.Parse("f57449ae-c365-4b0d-82f8-954c1dd562d7"), Amount = 50.75m, Description = "Groceries", Date = DateTime.Now, UserId = Guid.Parse("b3c5814b-9434-4520-9887-64d1979db9b0"), CategoryId = Guid.Parse("4df523f6-5e73-43af-aaf1-0c980e16fc9f") },
                new Expense { ExpenseId = Guid.Parse("7ef757a7-4494-4a52-a6d0-ec8c6b823910"), Amount = 15.00m, Description = "Bus Ticket", Date = DateTime.Now, UserId = Guid.Parse("b3c5814b-9434-4520-9887-64d1979db9b0"), CategoryId = Guid.Parse("67ecebc9-e619-4f84-97a4-f33f5b59790a") },
                new Expense { ExpenseId = Guid.Parse("635958b5-d605-4fef-8cae-6916fe7d162c"), Amount = 120.00m, Description = "Electric Bill", Date = DateTime.Now, UserId = Guid.Parse("5a5f8530-846c-4398-a264-1e6cabdd053e"), CategoryId = Guid.Parse("9d86390b-8e1b-4b82-baff-c0b953f1ef1e") }
            );

            base.OnModelCreating(modelBuilder);
        }

    }
}
