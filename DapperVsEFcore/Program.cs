using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.Roslyn;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
BenchmarkRunner.Run<BenchMark>();
var bench = new BenchMark();
var dapperResult = await bench.FetchProductsUsingDapper();
var EFResult = await bench.FetchProductsUsingEFCore();
var EFSQLResult = await bench.FetchProductsUsingEFSqlQuery();

Console.WriteLine($"Dapper: {dapperResult.Count()} products");
Console.WriteLine($"EF: {EFResult.Count()} products");
Console.WriteLine($"EF SQLQuery: {EFSQLResult.Count()} products");


app.Run();
