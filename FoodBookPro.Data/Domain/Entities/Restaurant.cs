namespace FoodBookPro.Data.Domain.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Specialty { get; set; }
        public string Description { get; set; }
        public TimeOnly OpeningTime { get; set; }
        public TimeOnly ClosingTime { get; set; }
        public bool Status { get; set; } // true = Open, false = Closed
        public int OwnerId { get; set; } //FK

        // Navigation Properties
        public User Owner { get; set; } // Navigation property to User entity (Owner of the restaurant)
        public ICollection<Table> Tables { get; set; } // Collection of tables in the restaurant
        public ICollection<Booking> Bookings { get; set; } // Collection of bookings for this restaurant
        public ICollection<MenuItem> MenuItems { get; set; } // Collection of menu items for this restaurant
        public ICollection<Order> Orders { get; set; } // Collection of orders for this restaurant
        public ICollection<Review> Reviews { get; set; } // Collection of reviews for this restaurant
    }
}
