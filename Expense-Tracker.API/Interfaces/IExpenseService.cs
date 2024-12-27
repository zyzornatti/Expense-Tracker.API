using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Interfaces
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetAllAsync(Guid userId, string? filterOn, string? filterQuery,string? sortBy,bool isAscending,int pageNumber,int pageSize);
        Task<Expense?> GetByIdAsync(Guid id);
        Task<Expense> CreateAsync(Expense expense);
        Task<Expense?> UpdateAsync(Guid id, Expense expense);
        Task<Expense?> DeleteAsync(Guid id);
        Task<decimal?> GetTotalExpensesAsync(Guid userId, DateTime startDate, DateTime endDate);
    }
}
