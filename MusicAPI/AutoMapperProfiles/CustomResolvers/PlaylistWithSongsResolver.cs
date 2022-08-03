namespace MusicAPI.AutoMapperProfiles.CustomResolvers
{
    public class PlaylistWithSongsResolver :
        IValueResolver<Playlist, PlaylistDetailedDTO, List<string>>
    {
        public List<string> Resolve(Playlist source, PlaylistDetailedDTO destination, List<string> destMember, ResolutionContext context)
        {
            var songNames = new List<string>();

            foreach (var song in source.Songs)
                songNames.Add(song.Name);

            return songNames;
        }
    }
}
