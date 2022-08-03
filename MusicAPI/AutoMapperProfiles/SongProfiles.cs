namespace MusicAPI.AutoMapperProfiles
{
    public class SongProfiles : Profile
    {
        public SongProfiles() 
        {
            CreateMap<Song, SongDetailedDTO>()
                .ForMember(dest => dest.SongName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Artist,
                opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.Album,
                opt => opt.MapFrom(src => src.Album.Name))
                .ForMember(dest => dest.Genre,
                opt => opt.MapFrom(dest => dest.Genre.Name))
                .ForMember(dest => dest.Playlists,
                opt => opt.MapFrom<SongWithPlaylistsResolver>());

            CreateMap<Song, SongSimpleDTO>()
                .ForMember(dest => dest.SongName,
                opt => opt.MapFrom(src => src.Name));
                
        }

    }
}
