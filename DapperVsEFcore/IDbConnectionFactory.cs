using System.Data;
namespace DapperVsEFcore;

public interface IDbConnectionFactory
{
    IDbConnection GetOpenConnection();
}
