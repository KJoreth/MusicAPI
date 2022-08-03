namespace MusicAPI.Data.DAL.Interfaces
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<List<Album>> GetAllWithArtistsAndSongsAsync();
        Task<Album> GetSingleByNameAsync(string name);
        Task<Album> GetSingleByNameWithArtistsAndSongsAsync(string name);
        Task<Album> GetSingleByNameWithArtistsAsync(string name);
    }
}
