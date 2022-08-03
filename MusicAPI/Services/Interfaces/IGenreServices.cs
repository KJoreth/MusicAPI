namespace MusicAPI.Services.Interfaces
{
    public interface IGenreServices
    {
        Task<GenreSimpleDTO> CreateNewAsync(string name);
        Task DeleteAsync(string name);
        Task<List<GenreSimpleDTO>> GetAllAsync();
        Task<GenreDetailedDTO> GetSingleDetailedAsync(string name);
        Task<GenreWithSongCountDTO> GetSingleAsync(string name);
        Task UpdateAsync(string name, string newName);
    }
}