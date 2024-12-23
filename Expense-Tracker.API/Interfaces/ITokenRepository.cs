using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Interfaces
{
    public interface ITokenRepository
    {
        string GenerateJwtToken(User user);
    }
}
