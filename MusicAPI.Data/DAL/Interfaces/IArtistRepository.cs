namespace MusicAPI.Data.DAL.Interfaces
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<List<Artist>> GetAllWithAlbumsAndSongsAsync();
        Task<Artist> GetSingleByNameAsync(string name);
        Task<Artist> GetSingleByNameWithAlbumsAndSongsAsync(string name);
    }
}
