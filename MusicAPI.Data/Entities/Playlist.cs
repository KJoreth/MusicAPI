namespace MusicAPI.Data.Entities
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public List<Song> Songs { get; set; } = new();


        public Playlist() { }
        public Playlist(int id, string name) 
        {
            PlaylistId = id;
            Name = name;
        }
    }
}
