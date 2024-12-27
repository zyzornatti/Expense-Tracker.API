using Expense_Tracker.API.Data;
using Expense_Tracker.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.API.Interfaces;

namespace Expense_Tracker.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public CategoryService(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (existingCategory != null)
            {
                _dbContext.Categories.Remove(existingCategory);
                await _dbContext.SaveChangesAsync();
                return existingCategory;
            }

            return null;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetById(Guid id)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (existingCategory != null)
            {
                return existingCategory;
            }

            return null;
        }

        public async Task<Category?> UpdateAsync(Guid id, Category category)
        {
            var existingCategory = await _dbContext.Categories.FirstOrDefaultAsync(x => x.CategoryId == id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                await _dbContext.SaveChangesAsync();
                return existingCategory;
            }

            return null;
        }
    }
}
