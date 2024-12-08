using System.Data;
using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.EntityFrameworkCore;
namespace DapperVsEFcore;

[MemoryDiagnoser]
public class BenchMark
{
    private readonly IDbConnectionFactory connectionFactory;
    private readonly ApplicationDbContext dbContext;

    public BenchMark()
    {
        // Simulate dependency injection
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        connectionFactory = new NpgsqlConnectionFactory(configuration);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        
        dbContext = new ApplicationDbContext(optionsBuilder.Options);
    }

    [Benchmark(Baseline = true)]
    public async Task<List<Product>> FetchProductsUsingDapper()
    {
        using var connection = connectionFactory.GetOpenConnection();
        const string sql = """
        SELECT * FROM "Products" WHERE "Price" > @Price ORDER BY "Name"
        """;
        var products = await connection.QueryAsync<Product>(
            sql,
            new { Price = 30 });
        return products.ToList();
    }

    [Benchmark]
    public async Task<List<Product>> FetchProductsUsingEFCore()
    {
        return await dbContext.Products
            .AsNoTracking()
            .Where(p => p.Price > 30) // Filtering: Price greater than 50
            .OrderBy(p => p.Name)    // Ordering: By Name
            .ToListAsync();
    }

  

    [Benchmark]
    public async Task<List<Product>> FetchProductsUsingEFSqlQuery()
    {
        int price = 30;
        return await dbContext.Database.SqlQuery<Product>(
            $"""
            SELECT * FROM "Products" WHERE "Price" > {price} ORDER BY "Name"
            """)
            .AsNoTracking()
            .ToListAsync();
    }
}
