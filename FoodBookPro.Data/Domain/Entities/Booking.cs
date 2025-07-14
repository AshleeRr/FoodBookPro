namespace FoodBookPro.Data.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK
        public int RestaurantId { get; set; } // FK
        public int TableId { get; set; } // FK
        public DateOnly BookingDate { get; set; }
        public TimeOnly BookingTime { get; set; }
        public int NumberOfGuests { get; set; }
        public int Status { get; set; } // 0 = Pending, 1 = Confirmed, 2 = Cancelled

        // Navigation Properties
        public User User { get; set; } // Navigation property to User entity
        public Restaurant Restaurant { get; set; } // Navigation property to Restaurant entity
        public Table Table { get; set; } // Navigation property to Table entity
    }
}
