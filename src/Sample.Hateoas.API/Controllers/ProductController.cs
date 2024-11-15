namespace Sample.Hateoas.API.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController(IProductRepository repository, ILinksService linksService) : ControllerBase
{
    [HttpPost("seed")]
    public async Task<IActionResult> Seed()
    {
        await repository.AddAsync(new ProductDto { Name = "seed 1", Price = 10 });
        await repository.AddAsync(new ProductDto { Name = "seed 2", Price = 10 });
        return Ok("Seed data added.");
    }

    [HttpGet(Name = "GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await repository.GetAllAsync();

        foreach (var product in products)
        {
            await linksService.AddLinksAsync(product);
        }

        return Ok(products);
    }

    [HttpGet("{id}", Name = "GetProductById")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await repository.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        await linksService.AddLinksAsync(product);

        return Ok(product);
    }

    [HttpPost(Name = "CreateProduct")]
    public async Task<IActionResult> CreateProduct(ProductDto product)
    {
        await repository.AddAsync(product);
        await linksService.AddLinksAsync(product);

        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
    }

    [HttpDelete("{id}", Name = "DeleteProductById")]
    public async Task<IActionResult> DeleteProductById(int id)
    {
        var success = await repository.DeleteAsync(id);
        return success ? NoContent() : NotFound();
    }

    [HttpPut("{id}", Name = "UpdateProductById")]
    public async Task<IActionResult> UpdateProductById(int id, ProductDto updatedProduct)
    {
        var success = await repository.UpdateAsync(id, updatedProduct);
        return success ? Ok(updatedProduct) : NotFound();
    }
}
