namespace MusicAPI.DTOs.SongDTOs
{
    public class SongDetailedDTO
    {
        public string SongName { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }

        public List<String> Playlists { get; set; }
    }
}
