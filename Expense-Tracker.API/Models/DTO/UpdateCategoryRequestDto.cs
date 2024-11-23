using System.ComponentModel.DataAnnotations;

namespace Expense_Tracker.API.Models.DTO
{
    public class UpdateCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
