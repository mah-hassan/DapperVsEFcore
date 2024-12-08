using System.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace DapperVsEFcore;

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string connectionString;

    public NpgsqlConnectionFactory(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException("Can not connect to server, No connection string found.");
    }

    public IDbConnection GetOpenConnection()
    {
        var connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}
