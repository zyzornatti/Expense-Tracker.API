using Expense_Tracker.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.API.Models.DTO
{
    public class AddCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }

        //Navigation properties
        [Required]
        public Guid UserId { get; set; }
    }
}
