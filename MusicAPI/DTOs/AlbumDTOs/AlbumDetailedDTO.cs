namespace MusicAPI.DTOs.AlbumDTOs
{
    public class AlbumDetailedDTO
    {
        public string AlbumName { get; set; }
        public string ArtistName { get; set; }
        public int SongCount { get; set; }
        public List<string> Songs { get; set; }

    }
}
