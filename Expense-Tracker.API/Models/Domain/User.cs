namespace Expense_Tracker.API.Models.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        //Navigation properties
        public ICollection<Expense> Expenses { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
