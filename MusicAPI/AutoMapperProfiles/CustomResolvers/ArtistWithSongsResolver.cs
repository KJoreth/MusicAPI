namespace MusicAPI.AutoMapperProfiles.CustomResolvers
{
    public class ArtistWithSongsResolver : 
        IValueResolver<Artist, ArtistDetailedDTO, List<AlbumWithSongsDTO>>
    {
                 
        public List<AlbumWithSongsDTO> Resolve(Artist source, ArtistDetailedDTO destination, List<AlbumWithSongsDTO> destMember, ResolutionContext context)
        {
            var albums = new List<AlbumWithSongsDTO>();

            foreach(var album in source.Albums) 
            {
                AlbumWithSongsDTO albumDTO = new AlbumWithSongsDTO();
                albumDTO.AlbumName = album.Name;

                foreach(var song in album.Songs)
                    albumDTO.Songs.Add(song.Name);

                albums.Add(albumDTO);                      
            }

            return albums;

        }
    }
}
