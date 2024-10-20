using E_Commerce.Models;

namespace AssignmentTaskECommerce.Models
{
    public class Product
    {
        public int ProductID { get; set; }  // Changed to ProductID for consistency
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ICollection<SalesItem> SalesItems { get; set; }  // Assuming SalesItem exists
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();  // Added CartItems collection
    }
}
