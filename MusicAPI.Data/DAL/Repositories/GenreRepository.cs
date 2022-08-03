namespace MusicAPI.Data.DAL.Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(APIContext context)
            : base(context) { }

        public APIContext APIContext
            => _dbContext as APIContext;

        public async Task<Genre> GetSingleByName(string name)
            => await APIContext.Genres
                .Where(x => x.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();

        public async Task<Genre> GetSingleWithSongsByName(string name)
            => await APIContext.Genres
                .Where(x => x.Name.ToLower() == name.ToLower())
                .Include(x => x.Songs)
                .FirstOrDefaultAsync();
    }
}
