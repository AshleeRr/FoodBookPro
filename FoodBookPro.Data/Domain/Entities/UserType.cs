namespace FoodBookPro.Data.Domain.Entities
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; } //Client | Owner | Admin
        // Navigation Properties
        public ICollection<User> Users { get; set; }
    }
}
