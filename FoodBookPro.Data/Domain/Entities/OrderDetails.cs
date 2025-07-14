namespace FoodBookPro.Data.Domain.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // FK
        public int MenuItemId { get; set; } // FK
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        // Navigation Properties
        public Order Order { get; set; } // Navigation property to Order entity
        public MenuItem MenuItem { get; set; } // Navigation property to MenuItem entity
    }
}
