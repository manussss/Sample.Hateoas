var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SampleHateoasConnection"), sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(
        maxRetryCount: 10,
        maxRetryDelay: TimeSpan.FromSeconds(30),
        errorNumbersToAdd: null);
}));

builder.Services.AddLinks(config =>
{
    config.AddPolicy<ProductDto>(policy =>
    {
        policy.RequireSelfLink()
            .RequireRoutedLink("all", nameof(ProductController.GetAllProducts), _ => new { })
            .RequireRoutedLink("delete", nameof(ProductController.DeleteProductById), _ => new { id = _.Id })
            .RequireRoutedLink("update", nameof(ProductController.UpdateProductById), _ => new { id = _.Id });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var applyMigrations = builder.Configuration.GetValue<bool>("APPLY_MIGRATIONS");

if (applyMigrations)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
