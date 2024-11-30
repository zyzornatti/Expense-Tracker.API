using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Models.DTO
{
    public class BudgetDto
    {
        public Guid BudgetId { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Amount { get; set; } // Budgeted amount
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
