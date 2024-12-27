using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
