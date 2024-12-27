using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateAsync(Category category);
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetById(Guid id);
        Task<Category?> UpdateAsync(Guid id, Category category);
        Task<Category?> DeleteAsync(Guid id);
    }
}
