namespace MusicAPI.DTOs.PlaylistDTOs
{
    public class PlaylistDetailedDTO
    {
        public string PlaylistName { get; set; }

        public int SongCount { get; set; }
        public List<string> Songs { get; set; }
    }
}
