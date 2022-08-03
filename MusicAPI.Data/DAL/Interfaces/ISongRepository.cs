namespace MusicAPI.Data.DAL.Interfaces
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<List<Song>> GetAllWithVariables();
        Task<Song> GetSingleWithPlaylistsByName(string name);
        Task<Song> GetSingleByNameWithVariablesAsync(string name);
        Task<Song> GetSingleByName(string name);
    }
}
