
namespace MusicAPI.Extensions
{
    static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection service, string connectionString) 
        {
            //Database
            service.AddDbContext<APIContext>(options => options.UseSqlServer(connectionString));

            //Repositories
            service.AddScoped<IAlbumRepository, AlbumRepository>();
            service.AddScoped<IArtistRepository, ArtistRepository>();
            service.AddScoped<IGenreRepository, GenreRepository>();
            service.AddScoped<IPlaylistRepository, PlaylistRepository>();
            service.AddScoped<ISongRepository, SongRepository>();

            //Services
            service.AddScoped<IAlbumServices, AlbumServices>();
            service.AddScoped<IArtistServices, ArtistServices>();
            service.AddScoped<IGenreServices, GenreServices>();
            service.AddScoped<IPlaylistServices, PlaylistServices>();
            service.AddScoped<ISongServices, SongServices>();

            //Unit of work
            service.AddScoped<IAPIUnitOfWork, APIUnitOfWork>();

            //Unit of services
            service.AddScoped<IUnitOfServices, UnitOfServices>();
        }
    }
}
