
using ProductApi.Models;

namespace ProductApi.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> _products = new()
        {
            new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 999.99m, Stock = 10 },
            new Product { Id = 2, Name = "Mouse", Category = "Electronics", Price = 25.50m, Stock = 50 },
            new Product { Id = 3, Name = "Chair", Category = "Furniture", Price = 150.00m, Stock = 25 }
        };

        private static int _nextId = 4;

        public async Task<List<Product>> GetAllProductsAsync()
        {
            await Task.Delay(100); // Simulate async
            return _products;
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            await Task.Delay(50);
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.Id = _nextId++;
            _products.Add(product);
            await Task.Delay(100);
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product updatedProduct)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return null;

            existing.Name = updatedProduct.Name;
            existing.Category = updatedProduct.Category;
            existing.Price = updatedProduct.Price;
            existing.Stock = updatedProduct.Stock;

            await Task.Delay(100);
            return existing;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return false;

            _products.Remove(existing);
            await Task.Delay(100);
            return true;
        }

    }
}
