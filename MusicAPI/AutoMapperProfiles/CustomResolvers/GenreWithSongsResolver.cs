namespace MusicAPI.AutoMapperProfiles.CustomResolvers
{
    public class GenreWithSongsResolver :
        IValueResolver<Genre, GenreDetailedDTO, List<string>>
    {
        public List<string> Resolve(Genre source, GenreDetailedDTO destination, List<string> destMember, ResolutionContext context)
        {
            var songNames = new List<string>();

            foreach (var song in source.Songs)
                songNames.Add(song.Name);

            return songNames;

        }
    }
}
