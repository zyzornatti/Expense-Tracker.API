using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> LoginAsync(User user);
        Task<User?> RegisterAsync(User details);
    }
}
