namespace MusicAPI.AutoMapperProfiles
{
    public class ArtistProfiles : Profile
    {
        public ArtistProfiles() 
        {
            CreateMap<Artist, ArtistSimpleDTO>()
                .ForMember(dest => dest.ArtistName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AlbumCount,
                opt => opt.MapFrom(src => src.Albums.Count))
                .ForMember(dest => dest.SongCount,
                opt => opt.MapFrom(dest => dest.Songs.Count));

            CreateMap<Artist, ArtistDetailedDTO>()
                .ForMember(dest => dest.ArtistName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Albums,
                opt => opt.MapFrom<ArtistWithSongsResolver>());
        }
    }
}
