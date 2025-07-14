using FoodBookPro.Data.Application.Enums;

namespace FoodBookPro.Data.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK
        public int RestaurantId { get; set; } // FK
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public Decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } // 0 = Ordered, 1 = Pending, 2 = Delivered, 3 = Cancelled

        // Navigation Properties
        public User User { get; set; } // Navigation property to User entity
        public Restaurant Restaurant { get; set; } // Navigation property to Restaurant entity
        public ICollection<OrderDetails> Details { get; set; } // Collection of order details for this order
        public Payment Payment { get; set; } // Navigation property to Payment entity
    }
}
