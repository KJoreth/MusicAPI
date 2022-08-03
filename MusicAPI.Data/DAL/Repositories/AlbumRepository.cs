namespace MusicAPI.Data.DAL.Repositories
{
    public class AlbumRepository : BaseRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(APIContext context) 
            : base(context) { }

        public APIContext APIContext
            => _dbContext as APIContext;

        public async Task<List<Album>> GetAllWithArtistsAndSongsAsync()
            => await APIContext.Albums
                .Include(x => x.Artist)
                .Include(x => x.Songs)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Album> GetSingleByNameWithArtistsAndSongsAsync(string name)
            => await APIContext.Albums
                .Where(x => x.Name.ToLower() == name.ToLower())
                .Include(x => x.Artist)
                .Include(x => x.Songs)
                .FirstOrDefaultAsync();

        public async Task<Album> GetSingleByNameWithArtistsAsync(string name)
            => await APIContext.Albums
                .Where(x => x.Name.ToLower() == name.ToLower())
                .Include(x => x.Artist)
                .FirstOrDefaultAsync();

        public async Task<Album> GetSingleByNameAsync(string name)
            => await APIContext.Albums
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();
    }
}
