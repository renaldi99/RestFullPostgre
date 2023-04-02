using Dapper;
using RestFullPostgre.Context;

namespace RestFullPostgre.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DapperContext _context;

        public GenericRepository(DapperContext context)
        {
            _context = context;
        }

        public int Delete(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = connection.Execute(query, param);
            return result;
        }

        public async Task<int> DeleteAsync(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = await connection.ExecuteAsync(query, param);
            return result;
        }

        public List<T> FindAllBy(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = connection.Query<T>(query, param);
            return result.ToList();
        }

        public async Task<List<T>> FindAllByAsync(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = await connection.QueryAsync<T>(query, param);
            return result.ToList();
        }

        public T FindBy(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = connection.Query<T>(query, param);
            return result.FirstOrDefault();
        }

        public async Task<T> FindByAsync(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = await connection.QueryAsync<T>(query, param);
            return result.FirstOrDefault();
        }

        public int Save(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = connection.Execute(query, param);
            return result;
        }

        public async Task<int> SaveAsync(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = await connection.ExecuteAsync(query, param);
            return result;

        }

        public int Update(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = connection.Execute(query, param);
            return result;
        }

        public async Task<int> UpdateAsync(string query, object param)
        {
            using var connection = _context.CreatConnection();
            var result = await connection.ExecuteAsync(query, param);
            return result;
        }
    }
}
