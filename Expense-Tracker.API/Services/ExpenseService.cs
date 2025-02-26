﻿using Expense_Tracker.API.Data;
using Expense_Tracker.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.API.Interfaces;

namespace Expense_Tracker.API.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ExpenseTrackerDbContext dbContext;

        public ExpenseService(ExpenseTrackerDbContext dbContext)
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

        public async Task<List<Expense>> GetAllAsync(Guid userId, string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1000)
        {
            var expenses = dbContext.Expenses
                .Where(e => e.UserId == userId)
                .Include("User")
                .Include("Category")
                .AsQueryable();

            //Filter
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Amount", StringComparison.OrdinalIgnoreCase))
                {
                    expenses = expenses.Where(x => x.Amount.ToString().Contains(filterQuery));
                }

                if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    expenses = expenses.Where(x => x.Description.Contains(filterQuery));
                }
            }

            //Sort
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Amount", StringComparison.OrdinalIgnoreCase))
                {
                    expenses = isAscending ? expenses.OrderBy(x => x.Amount) : expenses.OrderByDescending(x => x.Amount);
                }
                else if (sortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    expenses = isAscending ? expenses.OrderBy(x => x.Description) : expenses.OrderByDescending(x => x.Description);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await expenses.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            var existingExpense = await dbContext.Expenses
                                .Include("User")
                                .Include("Category")
                                .FirstOrDefaultAsync(x => x.ExpenseId == id);
            if (existingExpense == null)
            {
                return null;
            }
            return existingExpense;
        }

        public async Task<decimal?> GetTotalExpensesAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
             return await dbContext.Expenses
                    .Where(e => e.UserId == userId && e.Date >= startDate && e.Date <= endDate)
                    .SumAsync(e => e.Amount);       
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
