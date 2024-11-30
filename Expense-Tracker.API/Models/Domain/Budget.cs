namespace Expense_Tracker.API.Models.Domain
{
    public class Budget
    {
        public Guid BudgetId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid? CategoryId { get; set; } 
        public Category Category { get; set; }
        public decimal Amount { get; set; } // Budgeted amount
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
