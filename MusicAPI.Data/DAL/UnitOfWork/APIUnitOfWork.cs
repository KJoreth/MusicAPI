namespace MusicAPI.Data.DAL.UnitOfWork
{
    public class APIUnitOfWork : IAPIUnitOfWork
    {
        private readonly DbContext _context;

        public APIUnitOfWork(APIContext context, IAlbumRepository albumRepository, IArtistRepository artistRepository, 
            IGenreRepository genreRepository, IPlaylistRepository playlistRepository, ISongRepository songRepository) 
        {
            _context = context;
            AlbumRepository = albumRepository;
            ArtistRepository = artistRepository;
            GenreRepository = genreRepository;
            PlaylistRepository = playlistRepository;
            SongRepository = songRepository;
        }
        public IAlbumRepository AlbumRepository { get; private set; }

        public IArtistRepository ArtistRepository { get; private set; }

        public IGenreRepository GenreRepository { get; private set; }

        public IPlaylistRepository PlaylistRepository { get; private set; }

        public ISongRepository SongRepository { get; private set; }

        public async Task<int> CompleteAsync()
            => await Task.Run(() => _context.SaveChanges()); 

    }
}
