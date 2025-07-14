namespace FoodBookPro.Data.Domain.Entities
{
    public class Table
    {
        public int Id { get; set; }
        public int Seats { get; set; }
        public bool IsReserved { get; set; }
        public int RestaurantId { get; set; } // FK
        // Navigation Properties
        public Restaurant Restaurant { get; set; } // Navigation property to Restaurant entity
        public ICollection<Booking> Bookings { get; set; } // Collection of bookings for this table
    }
}
