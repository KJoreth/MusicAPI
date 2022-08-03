namespace MusicAPI.Data.DAL.Repositories
{
    public class SongRepository : BaseRepository<Song>, ISongRepository
    {
        public SongRepository(APIContext context)
            : base(context) { }

        public APIContext APIContext
            => _dbContext as APIContext;

        public async Task<Song> GetSingleByNameWithVariablesAsync(string name)
            => await APIContext.Songs
                .Where(x => x.Name.ToLower() == name.ToLower())
                .Include(x => x.Genre)
                .Include(x => x.Artist)
                .Include(x => x.Album)
                .Include(x => x.Playlists)
                .FirstOrDefaultAsync();

        public async Task<Song> GetSingleWithPlaylistsByName(string name)
        => await APIContext.Songs
                .Where(x => x.Name.ToLower() == name.ToLower())
                .Include(x => x.Playlists)
                .FirstOrDefaultAsync();

        public async Task<Song> GetSingleByName(string name)
        => await APIContext.Songs
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();

        public async Task<List<Song>> GetAllWithVariables()
            => await APIContext.Songs
                .Include(x => x.Artist)
                .Include(x => x.Playlists)
                .Include(x => x.Genre)
                .Include(x => x.Album)
                .ToListAsync();

    }
}
