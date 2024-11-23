namespace Expense_Tracker.API.Models.Domain
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        //Navigation properties
        public Guid UserId { get; set; }
        public User User { get; set; }

        public ICollection<Expense> Expenses { get; set; }
    }
}
