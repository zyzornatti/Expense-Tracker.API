using Expense_Tracker.API.Models.Domain;

namespace Expense_Tracker.API.Models.DTO
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

    }
}
