namespace MusicAPI.Services.Interfaces
{
    public interface IPlaylistServices
    {
        Task<PlaylistSimpleDTO> CreateNewAsync(string playlistName);
        Task DeleteAsync(string playlistName);
        Task<List<PlaylistSimpleDTO>> GetAllAsync();
        Task<PlaylistDetailedDTO> GetSingleDetailedAsync(string playlistName);
        Task<PlaylistWithSongCountDTO> GetSingleAsync(string playlistName);
        Task UpdateAsync(string playlistName, string newName);
    }
}