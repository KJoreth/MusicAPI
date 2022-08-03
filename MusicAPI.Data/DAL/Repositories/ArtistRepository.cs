namespace MusicAPI.Data.DAL.Repositories
{
    public class ArtistRepository : BaseRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(APIContext context)
            : base(context) { }

        public APIContext APIContext
            => _dbContext as APIContext;

        public async Task<Artist> GetSingleByNameAsync(string name)
            => await APIContext.Artists
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();

        public async Task<Artist> GetSingleByNameWithAlbumsAndSongsAsync
                (string name)
            => await APIContext.Artists
                .Where(x => x.Name.ToLower() == name.ToLower())
                .Include(x => x.Albums)
                .Include(x => x.Songs)
                .FirstOrDefaultAsync();

        public async Task<List<Artist>> GetAllWithAlbumsAndSongsAsync()
            => await APIContext.Artists
                .Include(x => x.Albums)
                .Include(x => x.Songs)
                .ToListAsync();
    }
}
