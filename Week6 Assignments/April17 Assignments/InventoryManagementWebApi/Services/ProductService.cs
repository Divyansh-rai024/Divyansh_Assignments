using System.Collections.Generic;
using System.Linq;

public class ProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    } 
    public List<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }
     
    public string AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
        return "Product Added Successfully";
    }
     
    public string UpdateProduct(int id, Product updatedProduct)
    {
        var product = _context.Products.Find(id);
        if (product == null)
            return "Product Not Found";

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        product.Quantity = updatedProduct.Quantity;

        _context.SaveChanges();
        return "Product Updated Successfully";
    }
     
    public string DeleteProduct(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
            return "Product Not Found";

        _context.Products.Remove(product);
        _context.SaveChanges();
        return "Product Deleted Successfully";
    }
}