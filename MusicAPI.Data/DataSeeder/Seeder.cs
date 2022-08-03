namespace MusicAPI.Data.DataSeeder
{
    public static class Seeder
    {
        public static void SeedMusicAPIData(this ModelBuilder builder) 
        {
            List<Artist> artists = new();

            artists.Add(new Artist(1,"Laura Marling"));
            artists.Add(new Artist(2,"Ariana Grande"));
            artists.Add(new Artist(3,"Grace Jones"));
            artists.Add(new Artist(4,"Ashley Campbell"));
            artists.Add(new Artist(5,"Astrud Gilberto"));

            List<Genre> genres = new();

            genres.Add(new Genre(1, "Goth / Gothic Rock"));
            genres.Add(new Genre(2, "Grunge"));
            genres.Add(new Genre(3, "Hardcore Punk"));
            genres.Add(new Genre(4, "Indie Rock"));
            genres.Add(new Genre(5, "Lo - fi"));


            List<Album> albums = new();

            albums.Add(new Album(1, 1, "Axis: Bold as Love"));
            albums.Add(new Album(2, 2, "Born in the U.S.A."));
            albums.Add(new Album(3, 3, "Kid A"));
            albums.Add(new Album(4, 4, "Goodbye Yellow Brick Road"));
            albums.Add(new Album(5, 5, "Trout Mask Replica"));

            List<Playlist> playlists = new();

            playlists.Add(new Playlist(1, "I Still See Your Shadows In My Room"));
            playlists.Add(new Playlist(2, "Special Sauce"));
            playlists.Add(new Playlist(3, "Not a cloud in sight"));
            playlists.Add(new Playlist(4, "Breathe in"));
            playlists.Add(new Playlist(5, "Best Life"));


            List<Song> songs = new();

  
            songs.Add(new Song(2, 1, 1, 1, "About Damn Time"));
            songs.Add(new Song(3, 1, 1, 1, "Running Up"));
            songs.Add(new Song(4, 1, 1, 1, "First Class"));
            songs.Add(new Song(5, 2, 2, 2, "Wait For U"));
            songs.Add(new Song(6, 2, 2, 2, "Me Porto Bonito"));
            songs.Add(new Song(7, 2, 2, 2, "Break My Soul"));
            songs.Add(new Song(8, 2, 2, 2, "Heat Waves"));
            songs.Add(new Song(9, 3, 3, 3, "Late Night Talking"));
            songs.Add(new Song(10, 3, 3, 3, "Jimmy Cooks"));
            songs.Add(new Song(11, 3, 3, 3, "Big Energy"));
            songs.Add(new Song(12, 3, 3, 3, "I Like You(A Happier Song)"));
            songs.Add(new Song(13, 4, 4, 4, "Wasted On You"));
            songs.Add(new Song(14, 4, 4, 4, "Bad Habit"));
            songs.Add(new Song(15, 4, 4, 4, "Titi Me Pregunto"));
            songs.Add(new Song(16, 4, 4, 4, "Sunroof"));
            songs.Add(new Song(17, 5, 5, 5, "The Kind Of Love We Make"));
            songs.Add(new Song(18, 5, 5, 5, "Stay"));
            songs.Add(new Song(19, 5, 5, 5, "Glimpse Of Us"));
            songs.Add(new Song(20, 5, 5, 5, "Numb Little Bug"));
       


            builder.Entity<Artist>().HasData(artists);
            builder.Entity<Genre>().HasData(genres);
            builder.Entity<Album>().HasData(albums);          
            builder.Entity<Playlist>().HasData(playlists);
            builder.Entity<Song>().HasData(songs);

            


        }
    }
}
