namespace MusicAPI.Data.DAL.Interfaces
{
    public interface IPlaylistRepository : IRepository<Playlist>
    {
        Task<List<Playlist>> GetAllWithSongsAsync();
        Task<Playlist> GetSingleByNameAsync(string name);
        Task<Playlist> GetSingleWithSongsByNameAsync(string name);
    }
}
