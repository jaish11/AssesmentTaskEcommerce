using AssignmentTaskECommerce.Models;
using AssignmentTaskECommerce.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AssignmentTaskECommerce.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;

        public CartService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get the cart for a specific user
        public Cart GetCart(int userId)
        {
            var cartItems = _context.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Product)  // Eagerly load product details
                .ToList();

            var cart = new Cart
            {
                CartItems = cartItems,
                TotalPrice = cartItems.Sum(ci => ci.Quantity * ci.Product.Price)
            };

            return cart;
        }

        // Add an item to the cart
        public void AddToCart(int userId, CartItem cartItem)
        {
            cartItem.UserId = userId;
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        // Update the quantity of a cart item
        public void UpdateQuantity(int userId, int productId, int quantity)
        {
            var item = _context.CartItems.FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == productId);
            if (item != null)
            {
                item.Quantity = quantity;
                _context.SaveChanges();
            }
        }

        // Remove an item from the cart
        public void RemoveItem(int userId, int productId)
        {
            var item = _context.CartItems.FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == productId);
            if (item != null)
            {
                _context.CartItems.Remove(item);
                _context.SaveChanges();
            }
        }

        // Apply a discount to the cart
        public Cart ApplyDiscount(int userId, string discountCode)
        {
            var discount = _context.Discounts.FirstOrDefault(d => d.Code == discountCode && d.IsActive);
            var cart = GetCart(userId);

            if (discount != null)
            {
                // Apply discount based on whether it's a fixed amount or percentage
                if (discount.Amount > 1)  // Assuming it's a fixed amount discount
                {
                    cart.DiscountedTotal = cart.TotalPrice - discount.Amount;
                }
                else  // Assuming it's a percentage-based discount
                {
                    cart.DiscountedTotal = cart.TotalPrice * (1 - discount.Amount);
                }
            }
            else
            {
                cart.DiscountedTotal = cart.TotalPrice;  // No discount applied
            }

            return cart;
        }
    }
}
