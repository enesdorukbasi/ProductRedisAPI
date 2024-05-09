using ProductRedisAPI.Models;

namespace ProductRedisAPI.Data;

public interface IProductData{
    Product? CreateProduct(Product product);
    IEnumerable<Product?>? GetAllProducts();
    Product? GetProductById(string id);
    Product? UpdateProduct(Product product);
    bool DeleteProductById(string id);
}