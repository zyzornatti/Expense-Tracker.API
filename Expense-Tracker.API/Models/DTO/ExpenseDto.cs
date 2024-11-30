namespace Expense_Tracker.API.Models.DTO
{
    public class ExpenseDto
    {
        public Guid ExpenseId { get; set; }
        public string? Category { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        //Navigation properties
        //public Guid? CategoryId { get; set; }



    }
}
