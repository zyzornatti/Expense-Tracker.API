using Expense_Tracker.API.Models.Domain;
using System.Globalization;

namespace Expense_Tracker.API.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> GetAllAsync(string? filterOn, string? filterQuery,string? sortBy,bool isAscending,int pageNumber,int pageSize);
        Task<Expense?> GetByIdAsync(Guid id);
        Task<Expense> CreateAsync(Expense expense);
        Task<Expense?> UpdateAsync(Guid id, Expense expense);
        Task<Expense?> DeleteAsync(Guid id);
    }
}
