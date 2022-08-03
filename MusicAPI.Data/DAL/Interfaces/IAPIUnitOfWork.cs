namespace MusicAPI.Data.DAL.Interfaces
{
    public interface IAPIUnitOfWork
    {
        IAlbumRepository AlbumRepository { get; }
        IArtistRepository ArtistRepository { get; }
        IGenreRepository GenreRepository { get; }
        IPlaylistRepository PlaylistRepository { get; }
        ISongRepository SongRepository { get; }

        Task<int> CompleteAsync();
    }
}
