namespace MusicAPI.Services
{
    public class UnitOfServices : IUnitOfServices
    {
        public UnitOfServices( IAlbumServices albumServices, ISongServices songServices, 
            IArtistServices artistServices, IGenreServices genreServices, IPlaylistServices playlistServices)
        {
            AlbumServices = albumServices;
            SongServices = songServices;
            ArtistServices = artistServices;
            GenreServices = genreServices;
            PlaylistServices = playlistServices;
        }

        public IAlbumServices AlbumServices { get; private set; }
        public ISongServices SongServices { get; private set; }
        public IArtistServices ArtistServices { get; private set; }
        public IGenreServices GenreServices { get; private set; }
        public IPlaylistServices PlaylistServices { get; private set; }
    }
}
