namespace MusicAPI.Data.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Song>? Songs { get; set; } = new();
        public Genre() { }
    
        public Genre(int id, string name) 
        {
            Id = id;
            Name = name;
        }
           

    }
}
