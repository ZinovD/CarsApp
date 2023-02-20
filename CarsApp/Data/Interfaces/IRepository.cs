namespace CarsApp.Data.Interfaces
{
    public interface IRepository<T>where T : class
    {
        Task<IEnumerable<T>> GetListAsync();
        Task<T?> GetAsync(int id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
