namespace FoodBookPro.Data.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK
        public string Message { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsRead { get; set; } // true = Read, false = Unread
        // Navigation Properties
        public User User { get; set; } // Navigation property to User entity
    }
}
