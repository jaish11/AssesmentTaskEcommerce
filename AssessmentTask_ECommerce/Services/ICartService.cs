using AssignmentTaskECommerce.Models;

namespace AssignmentTaskECommerce.Services
{
    public interface ICartService
    {
        Cart GetCart(int userId);
        void AddToCart(int userId, CartItem cartItem);
        void UpdateQuantity(int userId, int productId, int quantity);
        void RemoveItem(int userId, int productId);
        Cart ApplyDiscount(int userId, string discountCode);
    }
}
