using Expense_Tracker.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.API.Models.DTO
{
    public class UpdateExpenseRequestDto
    {
        [Required]
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        //Navigation properties
        [Required]
        public Guid CategoryId { get; set; }
    }
}
