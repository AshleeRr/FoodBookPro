namespace FoodBookPro.Data.Domain.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK
        public int RestaurantId { get; set; } // FK
        public int Rating { get; set; } // Rating out of 5
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        // Navigation Properties
        public User User { get; set; } // Navigation property to User entity
        public Restaurant Restaurant { get; set; } // Navigation property to Restaurant entity
    }
}
