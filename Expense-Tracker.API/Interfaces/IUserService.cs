using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Interfaces
{
    public interface IUserService
    {
        Task<User?> LoginAsync(User user);
        Task<User?> RegisterAsync(User details);
    }
}
