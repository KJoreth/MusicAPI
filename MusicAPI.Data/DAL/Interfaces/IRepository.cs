namespace MusicAPI.Data.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
    }
}
