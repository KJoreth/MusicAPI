namespace MusicAPI.Data.DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;

        public BaseRepository(DbContext dbContext)
            => _dbContext = dbContext;


        public async Task AddAsync(T entity)
            => await Task.Run(() => _dbContext.Set<T>().Add(entity));

        public async Task DeleteAsync(T entity)
            => await Task.Run(() => _dbContext.Set<T>().Remove(entity));

        public async Task<List<T>> GetAllAsync()
            => await _dbContext.Set<T>().ToListAsync();

        public async Task UpdateAsync(T entity)
            => await Task.Run(() => _dbContext.Set<T>().Update(entity));

    }
}
