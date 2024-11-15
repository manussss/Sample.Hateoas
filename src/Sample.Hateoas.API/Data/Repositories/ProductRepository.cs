namespace Sample.Hateoas.API.Data.Repositories;

public class ProductRepository(ProductDbContext dbContext) : IProductRepository
{
    public async Task<ProductDto> AddAsync(ProductDto product)
    {
        dbContext.Add(new Product(product.Name, product.Price));
        await dbContext.SaveChangesAsync();

        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await dbContext.Product.FindAsync(id);

        if (product is null)
            return false;

        dbContext.Remove(product);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await dbContext.Product.Select(p => p.ToDto()).AsNoTracking().ToListAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(int id)
    {
        return (await dbContext.Product.FindAsync(id))?.ToDto();
    }

    public async Task<bool> UpdateAsync(int id, ProductDto dto)
    {
        var product = await dbContext.Product.FindAsync(id);

        if (product is null)
            return false;

        product.Price = dto.Price;
        product.Name = dto.Name;

        dbContext.Update(product);
        await dbContext.SaveChangesAsync();

        return true;
    }
}
