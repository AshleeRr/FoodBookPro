using FoodBookPro.Data.Domain.Entities;

namespace FoodBookPro.Data.Application.ViewModels.Users
{
    public class SaveUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int Role { get; set; } //Client | Owner | Admin
        public bool Status { get; set; } = false; //User is Unactivaded until he confirm his email

        public List<UserType> UserTypes { get; set; } = new List<UserType>(); // List of user types for dropdown selection
    }
}
