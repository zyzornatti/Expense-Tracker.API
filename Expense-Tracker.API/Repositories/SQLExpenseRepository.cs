using Expense_Tracker.API.Data;
using Expense_Tracker.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.API.Repositories
{
    public class SQLExpenseRepository : IExpenseRepository
    {
        private readonly ExpenseTrackerDbContext dbContext;

        public SQLExpenseRepository(ExpenseTrackerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Expense> CreateAsync(Expense expense)
        {
            await dbContext.Expenses.AddAsync(expense);
            await dbContext.SaveChangesAsync();
            return expense;
        }

        public async Task<Expense?> DeleteAsync(Guid id)
        {
            var existingExpense = await dbContext.Expenses.FirstOrDefaultAsync(x => x.ExpenseId == id);
            if (existingExpense == null)
            {
                return null;
            }
            dbContext.Expenses.Remove(existingExpense);
            await dbContext.SaveChangesAsync();
            return existingExpense;
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await dbContext.Expenses.ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            var existingExpense = await dbContext.Expenses.FirstOrDefaultAsync(x => x.ExpenseId == id);
            if (existingExpense == null)
            {
                return null;
            }
            return existingExpense;
        }

        public async Task<Expense?> UpdateAsync(Guid id, Expense expense)
        {
            var existingExpense = await dbContext.Expenses.FirstOrDefaultAsync(x => x.ExpenseId == id);
            if (existingExpense == null)
            {
                return null;
            }

            existingExpense.Amount = expense.Amount;
            existingExpense.Description = expense.Description;
            existingExpense.CategoryId = expense.CategoryId;

            await dbContext.SaveChangesAsync();
            return existingExpense;            
        }
    }
}
