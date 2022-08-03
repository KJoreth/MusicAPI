namespace MusicAPI.Data.DAL.Repositories
{
    public class PlaylistRepository : BaseRepository<Playlist>, IPlaylistRepository
    {
        public PlaylistRepository(APIContext context)
            : base(context) { }

        public APIContext APIContext
            => _dbContext as APIContext;

        public async Task<List<Playlist>> GetAllWithSongsAsync()
            => await APIContext.Playlists
                .Include(x => x.Songs)
                .ToListAsync();

        public async Task<Playlist> GetSingleWithSongsByNameAsync(string name)
            => await APIContext.Playlists
                .Where(x => x.Name.ToLower() == name.ToLower())
                .Include(x => x.Songs)
                .FirstOrDefaultAsync();

        public async Task<Playlist> GetSingleByNameAsync(string name)
            => await APIContext.Playlists
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();
    }
}
