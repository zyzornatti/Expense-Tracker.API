namespace Expense_Tracker.API.Models.DTO
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
