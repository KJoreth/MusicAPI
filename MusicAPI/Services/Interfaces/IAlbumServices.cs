namespace MusicAPI.Services.Interfaces
{
    public interface IAlbumServices
    {
        Task CreateNewAsync(AlbumCreateAndUpdateDTO model);
        Task DeleteAsync(string albumName);
        Task<List<AlbumSimpleDTO>> GetAllAsync();
        Task<AlbumSimpleDTO> GetSingleAsync(string albumName);
        Task<AlbumDetailedDTO> GetSingleDetailedAsync(string albumName);
        Task UpdateAsync(string albumName, AlbumCreateAndUpdateDTO model);
    }
}