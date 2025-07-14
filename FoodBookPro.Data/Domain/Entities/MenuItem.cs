namespace FoodBookPro.Data.Domain.Entities
{
    public class MenuItem
    {
        public int ItemId { get; set; }
        public int RestaurantId { get; set; } // FK
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; } // true = Available, false = Unavailable

        //Navigation Properties
        public Restaurant Restaurant { get; set; } // Navigation property to Restaurant entity
        public ICollection<OrderDetails> OrderDetails { get; set; } // Collection of order details associated with this menu item
    }
}
