namespace MusicAPI.Data.Entities
{
    public class Artist
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Song>? Songs { get; set; } = new();
        public List<Album>? Albums { get; set; } = new();

        public Artist() { }

        public Artist(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
