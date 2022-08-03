namespace MusicAPI.AutoMapperProfiles.CustomResolvers
{  
    public class SongWithPlaylistsResolver :
    IValueResolver<Song, SongDetailedDTO, List<string>>
    {
        public List<string> Resolve(Song source, SongDetailedDTO destination, List<string> destMember, ResolutionContext context)
        {
            var playlistNames = new List<string>();
    
            foreach (var playlist in source.Playlists)
                playlistNames.Add(playlist.Name);
    
            return playlistNames;
    
        }
    }
    
}
