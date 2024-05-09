using Microsoft.AspNetCore.Mvc;
using ProductRedisAPI.Models;
using ProductRedisAPI.Data;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly IProductData _repo;

    public ProductController(IProductData repo)
    {
        _repo = repo;
    }

    [HttpPost("create-product")]
    public IActionResult CreateProduct(Product product)
    {
        return Ok(_repo.CreateProduct(product));
    }

    [HttpGet("get-products")]
    public IActionResult GetProducts()
    {
        return Ok(_repo.GetAllProducts());
    }

    [HttpGet("get-product")]
    public IActionResult GetProducts(string id)
    {
        return Ok(_repo.GetProductById(id));
    }

    [HttpPut("update-product")]
    public IActionResult UpdateProduct(Product product)
    {
        return Ok(_repo.UpdateProduct(product));
    }

    [HttpDelete("delete-product")]
    public IActionResult DeleteProduct(string id)
    {
        var result = _repo.DeleteProductById(id);
        if (result == true) return Ok();
        else return NotFound();
    }
}