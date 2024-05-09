using System.Text.Json;
using ProductRedisAPI.Models;
using StackExchange.Redis;

namespace ProductRedisAPI.Data;

public class ProductData : IProductData
{
    private readonly IDatabase _database;

    public ProductData(IConnectionMultiplexer connection)
    {
        _database = connection.GetDatabase();
    }

    public Product? CreateProduct(Product product)
    {
        product.ID = $"product:{Guid.NewGuid().ToString()}";
        _database.HashSet("productdb", new HashEntry[]{
            new HashEntry(product.ID, JsonSerializer.Serialize(product))
        });
        return product;
    }

    public IEnumerable<Product?>? GetAllProducts()
    {
        return Array.ConvertAll(
            _database.HashGetAll("productdb"), product =>
            {
                return JsonSerializer.Deserialize<Product>(product.Value.ToString());
            }
        ).ToList();
    }

    public Product? GetProductById(string id)
    {
        return JsonSerializer.Deserialize<Product>(_database.HashGet("productdb", $"product:{id}").ToString());
    }

    public Product? UpdateProduct(Product product)
    {
        product.ID = $"product:{product.ID}";
        _database.HashSet("productdb", new HashEntry[]{
            new HashEntry(product.ID, JsonSerializer.Serialize(product))
        });
        return product;
    }

    public bool DeleteProductById(string id)
    {
        return _database.HashDelete("productdb", $"product:{id}");
    }
}