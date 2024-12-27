using Expense_Tracker.API.Data;
using Expense_Tracker.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.API.Interfaces;

namespace Expense_Tracker.API.Services
{
    public class UserService : IUserService
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public UserService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> LoginAsync(User user)
        {
            // Find user by username
            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (existingUser == null) return null;

            // Verify password
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(existingUser, existingUser.PasswordHash, user.PasswordHash);
            if (result != PasswordVerificationResult.Success) return null;

            return existingUser;

        }

        public async Task<User?> RegisterAsync(User details)
        {
            // Check if user already exists
            var isUserExist = await _dbContext.Users.AnyAsync(u => u.Username == details.Username || u.Email == details.Email);
            if (isUserExist)
            {
                return null;
            }

            // Hash the password
            var hasher = new PasswordHasher<User>();
            var newUser = new User
            {
                Username = details.Username,
                Email = details.Email,
            };
            newUser.PasswordHash = hasher.HashPassword(newUser, details.PasswordHash);

            // Save user to database
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;

        }
    }
}
