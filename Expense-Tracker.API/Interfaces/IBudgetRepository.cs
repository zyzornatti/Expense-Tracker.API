using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;

namespace Expense_Tracker.API.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget> CreateBudgetAsync(Budget budget);
        Task<List<Budget>> GetAllBudgetAsync(Guid user);
        Task<Budget> UpdateBudgetAsync(Guid id, Budget budget);
        Task<Budget> DeleteBudgetAsync(Guid id);
        Task<List<BudgetInsightDto>> GetBudgetInsights(Guid userId, DateTime startDate, DateTime endDate, Guid? categoryId);
    }
}
