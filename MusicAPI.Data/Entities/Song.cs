namespace MusicAPI.Data.Entities
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public int AlbumId { get; set; }
        public Album Album { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public List<Playlist> Playlists { get; set; } = new();

        public Song() { }
        public Song(int id, int artistId, int albumId, int genreId, string name) 
        {
            SongId = id;
            ArtistId = artistId;
            AlbumId = albumId;
            GenreId = genreId;
            Name = name;
        }

        
    }
}
