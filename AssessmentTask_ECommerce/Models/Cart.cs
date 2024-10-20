namespace AssignmentTaskECommerce.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountedTotal { get; set; }  // Add this property
        public List<CartItem> CartItems { get; set; }  // List of cart items
    }
}
