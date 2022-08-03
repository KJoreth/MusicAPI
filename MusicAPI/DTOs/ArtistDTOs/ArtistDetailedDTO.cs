namespace MusicAPI.DTOs.ArtistDTOs
{
    public class ArtistDetailedDTO
    {
        public string ArtistName { get; set; }
        public List<AlbumWithSongsDTO> Albums { get; set; }
    }
}
