namespace Expense_Tracker.API.Models.Domain
{
    public class Expense
    {
        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        //Navigation properties
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid? CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
