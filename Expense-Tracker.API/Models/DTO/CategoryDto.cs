using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Models.DTO
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        //Navigation properties
        public Guid UserId { get; set; }
        //public User User { get; set; }

        //public ICollection<Expense> Expenses { get; set; }
    }
}
