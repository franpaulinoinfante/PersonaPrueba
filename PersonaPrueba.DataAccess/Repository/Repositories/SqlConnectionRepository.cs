using System.Configuration;
using System.Data.SqlClient;

namespace PersonaPrueba.DataAccess.Repository.Repositories
{
    public abstract class SqlConnectionRepository
    {
        private readonly string _connectionString;

        protected SqlConnectionRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        }

        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
