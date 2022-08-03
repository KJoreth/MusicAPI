namespace MusicAPI.DTOs.SongDTOs
{
    public class SongCreateAndUpdateDTO
    {
        [Required]
        public string SongName { get; set; }
        [Required]
        public string Album { get; set; }
        [Required]
        public string Genre { get; set; }
        public List<String> Playlists { get; set; }
    }
}
