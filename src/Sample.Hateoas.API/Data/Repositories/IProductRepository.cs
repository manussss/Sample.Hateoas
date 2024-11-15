namespace Sample.Hateoas.API.Data.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(int id);
    Task<ProductDto> AddAsync(ProductDto product);
    Task<bool> UpdateAsync(int id, ProductDto product);
    Task<bool> DeleteAsync(int id);
}
