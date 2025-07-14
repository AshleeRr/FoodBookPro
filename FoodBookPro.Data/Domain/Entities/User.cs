namespace FoodBookPro.Data.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; } //Client | Owner | Admin
        public bool Status { get; set; }

        // Navigation Properties
        public UserType UserType { get; set; }
        public Restaurant? Restaurant { get; set; } // One-to-One relationship with Restaurant
        public ICollection<Booking>? Bookings { get; set; } // Collection of bookings made by the user
        public ICollection<Order>? Orders { get; set; } // Collection of orders made by the user
        public ICollection<Review>? Reviews { get; set; } // Collection of reviews made by the user
        public ICollection<Notification>? Notifications { get; set; } // Collection of notifications for the user
    }
}
