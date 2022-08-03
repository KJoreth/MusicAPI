namespace MusicAPI.AutoMapperProfiles
{
    public class GenreProfiles : Profile
    {
        public GenreProfiles() 
        {
            CreateMap<Genre, GenreSimpleDTO>()
                .ForMember(dest => dest.GenreName,
                opt => opt.MapFrom(src => src.Name));

            CreateMap<Genre, GenreWithSongCountDTO>()
                .ForMember(dest => dest.GenreName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SongCount,
                opt => opt.MapFrom(src => src.Songs.Count));

            CreateMap<Genre, GenreDetailedDTO>()
                .ForMember(dest => dest.GenreName,
                opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Songs,
                opt => opt.MapFrom<GenreWithSongsResolver>());
        }
    }
}
