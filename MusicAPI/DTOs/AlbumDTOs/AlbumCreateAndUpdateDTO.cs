namespace MusicAPI.DTOs.AlbumDTOs
{
    public class AlbumCreateAndUpdateDTO
    {
        [Required]
        public string AlbumName { get; set; }
        [Required]
        public string ArtistName { get; set; }
    }
}
