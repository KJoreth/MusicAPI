namespace MusicAPI.Services.Interfaces
{
    public interface IArtistServices
    {
        Task CreateNewAsync(string name);
        Task DeleteAsync(string name);
        Task<List<ArtistSimpleDTO>> GetAllAsync();
        Task<ArtistDetailedDTO> GetSingleDetailedAsync(string name);
        Task<ArtistSimpleDTO> GetSingleAsync(string name);
        Task UpdateAsync(string name, string newName);
    }
}