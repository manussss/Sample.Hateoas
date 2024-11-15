namespace Sample.Hateoas.API.Application.Dtos;

public class ProductDto : LinkContainer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }

    public ProductDto(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public ProductDto()
    {
        
    }
}
