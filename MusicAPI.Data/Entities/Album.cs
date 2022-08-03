namespace MusicAPI.Data.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }

        public List<Song> Songs { get; set; } = new();

        public Album() { }
        public Album(int id, int artistId, string name)
        {
            Id = id;
            ArtistId = artistId;
            Name = name;    
        }
    }
}
