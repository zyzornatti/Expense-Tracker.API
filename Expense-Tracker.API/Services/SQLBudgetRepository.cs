using Expense_Tracker.API.Data;
using Expense_Tracker.API.Models.Domain;
using Expense_Tracker.API.Models.DTO;
using Expense_Tracker.API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.API.Services
{
    public class SQLBudgetRepository : IBudgetRepository
    {
        private readonly ExpenseTrackerDbContext _dbContext;

        public SQLBudgetRepository(ExpenseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Budget> CreateBudgetAsync(Budget budget)
        {
            await _dbContext.Budgets.AddAsync(budget);
            await _dbContext.SaveChangesAsync();
            return budget;
        }

        public Task<Budget> DeleteBudgetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Budget>> GetAllBudgetAsync(Guid user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BudgetInsightDto>> GetBudgetInsights(Guid userId, DateTime startDate, DateTime endDate, Guid? categoryId)
        {
            var budgets = await _dbContext.Budgets
                    .Include(b => b.Category)
                    .Where(b => b.UserId == userId
                                && b.StartDate <= endDate
                                && b.EndDate >= startDate
                                && (categoryId == null || b.CategoryId == categoryId))
                    .ToListAsync();

            var expenses = await _dbContext.Expenses
                .Where(e => e.UserId == userId
                            && e.Date >= startDate
                            && e.Date <= endDate
                            && (categoryId == null || e.CategoryId == categoryId))
                .ToListAsync();

            var insights = budgets.Select(budget => new BudgetInsightDto
            {
                BudgetId = budget.BudgetId,
                Category = budget.Category?.Name,
                BudgetAmount = budget.Amount,
                TotalExpenses = expenses.Where(e => e.CategoryId == budget.CategoryId).Sum(e => e.Amount),
                RemainingAmount = budget.Amount - expenses.Where(e => e.CategoryId == budget.CategoryId).Sum(e => e.Amount),
                IsOverBudget = expenses.Where(e => e.CategoryId == budget.CategoryId).Sum(e => e.Amount) > budget.Amount
            }).ToList();

            return insights;
        }

        public Task<Budget> UpdateBudgetAsync(Guid id, Budget budget)
        {
            throw new NotImplementedException();
        }
    }
}
