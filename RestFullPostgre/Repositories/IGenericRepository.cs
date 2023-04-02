namespace RestFullPostgre.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> SaveAsync(string query, object param);
        Task<int> UpdateAsync(string query, object param);
        Task<int> DeleteAsync(string query, object param);
        Task<T> FindByAsync(string query, object param);
        Task<List<T>> FindAllByAsync(string query, object param);
        int Save(string query, object param);
        int Update(string query, object param);
        int Delete(string query, object param);
        T FindBy(string query, object param);
        List<T> FindAllBy(string query, object param);
    }
}
