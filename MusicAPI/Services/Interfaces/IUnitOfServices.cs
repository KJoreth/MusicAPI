namespace MusicAPI.Services.Interfaces
{
    public interface IUnitOfServices
    {
        IAlbumServices AlbumServices { get; }
        ISongServices SongServices { get; }
        IArtistServices ArtistServices { get; }
        IGenreServices GenreServices { get; }
        IPlaylistServices PlaylistServices { get; }
    }
}