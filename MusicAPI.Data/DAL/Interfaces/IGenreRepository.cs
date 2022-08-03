namespace MusicAPI.Data.DAL.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<Genre> GetSingleByName(string name);
        Task<Genre> GetSingleWithSongsByName(string name);
    }
}
