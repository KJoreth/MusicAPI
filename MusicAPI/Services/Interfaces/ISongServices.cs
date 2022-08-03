namespace MusicAPI.Services.Interfaces
{
    public interface ISongServices
    {
        Task<SongDetailedDTO> CreateNewAsync(SongCreateAndUpdateDTO model);
        Task DeleteAsync(string songName);
        Task<List<SongSimpleDTO>> GetAllAsync();
        Task<SongDetailedDTO> GetSingleAsync(string songName);
        Task<SongDetailedDTO> GetSingleDetailed(string songName);
        Task<SongDetailedDTO> UpdateAsync(string songName, SongCreateAndUpdateDTO model);
    }
}