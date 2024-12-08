#DapperVsEFcore

## Strat
### **Note**: I am using PostgreSQL as a database engine. install it first or go with your own engine, but do not forget to update appSettings.json file

### Run Update Datebase Command in CLI
```
dotnet ef database update -p DapperVsEFcore
```

### Run the project in release mode
```
dotnet run -c release -p DapperVsEFcore
```


## BenchMark Results
```

BenchmarkDotNet v0.14.0, Windows 10 (10.0.19045.5131/22H2/2022Update)
Intel Core i7-4810MQ CPU 2.80GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
.NET SDK 9.0.101
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2


```
| Method                       | Mean     | Error     | StdDev    | Ratio | RatioSD | Gen0    | Allocated | Alloc Ratio |
|----------------------------- |---------:|----------:|----------:|------:|--------:|--------:|----------:|------------:|
| FetchProductsUsingDapper     | 2.911 ms | 0.0546 ms | 0.0510 ms |  1.00 |    0.02 | 11.7188 | 162.39 KB |        1.00 |
| FetchProductsUsingEFCore     | 3.062 ms | 0.0287 ms | 0.0268 ms |  1.05 |    0.02 |  7.8125 | 243.56 KB |        1.50 |
| FetchProductsUsingEFSqlQuery | 3.073 ms | 0.0547 ms | 0.0884 ms |  1.06 |    0.03 |  7.8125 | 244.19 KB |        1.50 |
