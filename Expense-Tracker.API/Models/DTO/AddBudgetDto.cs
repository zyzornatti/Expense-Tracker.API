namespace Expense_Tracker.API.Models.DTO
{
    public class AddBudgetDto
    {
        public Guid? CategoryId { get; set; } // Optional: For category-specific budgets
        public decimal Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
