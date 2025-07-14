namespace FoodBookPro.Data.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // FK to Order
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // e.g., Credit Card, PayPal, etc.
        public bool Status { get; set; } // true = Completed, false = Pending  
        // Navigation Properties
        public Order Order { get; set; } // Navigation property to Order entity
    }
}
