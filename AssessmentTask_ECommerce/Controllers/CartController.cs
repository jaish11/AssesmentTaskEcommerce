using Microsoft.AspNetCore.Mvc;
using AssignmentTaskECommerce.Models;  // Ensure this is correct for Product
using AssignmentTaskECommerce.Services;  // For ICartService and ProductService
using AssignmentTaskECommerce.Data;  // For ApplicationDbContext

using System.Linq;  
using E_Commerce.Services;  // Correct namespace for your service

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // Get Cart for a specific user
        [HttpGet("{userId}")]
        public ActionResult<Cart> GetCart(int userId)
        {
            var cart = _cartService.GetCart(userId);
            if (cart == null)
                return NotFound();
            return Ok(cart);
        }

        // Add a product to the cart
        [HttpPost("{userId}")]
        public IActionResult AddToCart(int userId, [FromBody] CartItem cartItem)
        {
            _cartService.AddToCart(userId, cartItem);
            return Ok();
        }

        // Update product quantity in the cart
        [HttpPut("update/{userId}")]
        public IActionResult UpdateQuantity(int userId, [FromBody] CartItemUpdateModel updateModel)
        {
            _cartService.UpdateQuantity(userId, updateModel.ProductId, updateModel.Quantity);
            return Ok();
        }

        // Remove a product from the cart
        [HttpDelete("remove/{userId}/{productId}")]
        public IActionResult RemoveItem(int userId, int productId)
        {
            _cartService.RemoveItem(userId, productId);
            return Ok();
        }

        // Apply discount to the cart
        [HttpPost("discount/{userId}")]
        public IActionResult ApplyDiscount(int userId, [FromBody] DiscountApplyModel discountModel)
        {
            var cart = _cartService.ApplyDiscount(userId, discountModel.DiscountCode);
            return Ok(cart);
        }
    }

    // Helper class for updating quantity
    public class CartItemUpdateModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    // Helper class for applying discount
    public class DiscountApplyModel
    {
        public string DiscountCode { get; set; }
    }
}
