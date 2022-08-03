namespace MusicAPI.AutoMapperProfiles
{
    public class PlaylistProfiles : Profile
    {
        public PlaylistProfiles() 
        {
            CreateMap<Playlist, PlaylistSimpleDTO>()
                .ForMember(dest => dest.PlaylistName,
                opt => opt.MapFrom(src => src.Name));

            CreateMap<Playlist, PlaylistWithSongCountDTO>()
                .ForMember(dest => dest.PlaylistName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SongCount,
                opt => opt.MapFrom(src => src.Songs.Count()));

            CreateMap<Playlist, PlaylistDetailedDTO>()
                .ForMember(dest => dest.PlaylistName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SongCount,
                opt => opt.MapFrom(src => src.Songs.Count()))
                .ForMember(dest => dest.Songs,
                opt => opt.MapFrom<PlaylistWithSongsResolver>());
        }
    }
}
