namespace MusicAPI.AutoMapperProfiles
{
    public class AlbumProfiles : Profile
    {
        public AlbumProfiles() 
        {
            CreateMap<Album, AlbumSimpleDTO>()
                .ForMember(dest => dest.AlbumName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SongCount,
                opt => opt.MapFrom(src => src.Songs.Count()))
                .ForMember(dest => dest.ArtistName,
                opt => opt.MapFrom(src => src.Artist.Name));

            CreateMap<Album, AlbumDetailedDTO>()
                .ForMember(dest => dest.AlbumName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SongCount,
                opt => opt.MapFrom(src => src.Songs.Count()))
                .ForMember(dest => dest.ArtistName,
                opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.Songs,
                opt => opt.MapFrom<AlbumWithSongsResolver>());

       

         
        }
    }
}
