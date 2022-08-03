namespace MusicAPI.DTOs.AlbumDTOs
{
    public class AlbumWithSongsDTO
    {
        public string AlbumName { get; set; }
        public List<string> Songs { get; set; } = new List<string>();
    }
}
