namespace Sample.Hateoas.API.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Product { get; set; }
}
