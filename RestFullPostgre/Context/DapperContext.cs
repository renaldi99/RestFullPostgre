using Npgsql;
using System.Data;

namespace RestFullPostgre.Context
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(GlobalVariableList connectionString)
        {
            _connectionString = connectionString._connextion_SQL_Trancode;
        }

        public IDbConnection CreatConnection() => new NpgsqlConnection(_connectionString);
    }
}
