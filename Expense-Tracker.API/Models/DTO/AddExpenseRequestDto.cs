using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.API.Models.DTO
{
    public class AddExpenseRequestDto
    {
        [Required]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid CategoryId { get; set; }


    }
}
