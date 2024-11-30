namespace Expense_Tracker.API.Models.DTO
{
    public class BudgetInsightDto
    {
        public Guid BudgetId { get; set; }
        public string Category { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal RemainingAmount { get; set; }
        public bool IsOverBudget { get; set; }
    }
}
