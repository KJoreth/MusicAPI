namespace MusicAPI.AutoMapperProfiles.CustomResolvers
{
    public class AlbumWithSongsResolver : IValueResolver<Album, AlbumDetailedDTO, List<string>>
    {
        public List<string> Resolve(Album source, AlbumDetailedDTO destination, List<string> destMember, ResolutionContext context)
        {
            var songNames = new List<string>();

            foreach(var song in source.Songs)
                songNames.Add(song.Name);

            return songNames;
        }
    }
}
