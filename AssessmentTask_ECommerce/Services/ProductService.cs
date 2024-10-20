using Microsoft.EntityFrameworkCore;
using AssignmentTaskECommerce.Models;
using AssignmentTaskECommerce.Data;
using Microsoft.Extensions.Logging;

namespace E_Commerce.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductService> _logger;  // Dependency Injection for logging

        public ProductService(ApplicationDbContext context, ILogger<ProductService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                return await _context.Products
                    .Include(p => p.CartItems)  // Eagerly load CartItems
                    .Include(p => p.SalesItems)  // Eagerly load SalesItems
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching products.");
                throw new ApplicationException("An error occurred while fetching products", ex);
            }
        }

       public async Task<Product> GetProductByIdAsync(int id)
{
    try
    {
        return await _context.Products
            .Include(p => p.CartItems)
            .Include(p => p.SalesItems)
            .FirstOrDefaultAsync(p => p.ProductID == id);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "An error occurred while fetching the product by ID.");
        throw new ApplicationException("An error occurred while fetching the product by ID", ex);
    }
}
        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding the product.");
                throw new ApplicationException("An error occurred while adding the product", ex);
            }
        }

        public async Task<Product> UpdateProductAsync(Product updatedProduct)
        {
            try
            {
                var existingProduct = await _context.Products.FindAsync(updatedProduct.ProductID);

                if (existingProduct == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found during update.", updatedProduct.ProductID);
                    return null; // Return null if product is not found
                }

                // Update only existing fields
                existingProduct.ProductName = updatedProduct.ProductName;
                existingProduct.Price = updatedProduct.Price;

                _context.Products.Update(existingProduct); // Attach and mark as modified
                await _context.SaveChangesAsync();

                return existingProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product.");
                throw new ApplicationException("An error occurred while updating the product", ex);
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    _logger.LogWarning("Product with ID {ProductId} not found during delete.", id);
                    return false;
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product.");
                throw new ApplicationException("An error occurred while deleting the product", ex);
            }
        }
    }
}
