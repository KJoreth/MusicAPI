namespace MusicAPI.Services.Components
{
    public class SongServices : ISongServices
    {
        private readonly IAPIUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SongServices(IAPIUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<SongSimpleDTO>> GetAllAsync() 
        {
            List<Song> songs = await _unitOfWork.SongRepository.GetAllWithVariables();
            List<SongSimpleDTO> songDTOs = new List<SongSimpleDTO>();

            songs.ForEach(x => songDTOs.Add(_mapper.Map<SongSimpleDTO>(x)));

            return songDTOs;
        }

        public async Task<SongDetailedDTO> GetSingleAsync(string name) 
        {
            Song targetSong = await _unitOfWork.SongRepository.GetSingleByNameWithVariablesAsync(name);

            return _mapper.Map<SongDetailedDTO>(targetSong);
        }

        public async Task<SongDetailedDTO> CreateNewAsync(SongCreateAndUpdateDTO model)
        {
            Song song = new Song();
            Album targetAlbum = await _unitOfWork.AlbumRepository.GetSingleByNameWithArtistsAsync(model.Album);
            Genre targetGenre = await _unitOfWork.GenreRepository.GetSingleByName(model.Genre);
            Artist targetArtist = targetAlbum.Artist;
            List<Playlist> playlists = new List<Playlist>();

            foreach(var playlist in model.Playlists) 
            {
                Playlist targetPlaylist = await _unitOfWork.PlaylistRepository.GetSingleByNameAsync(playlist);;
                playlists.Add(targetPlaylist);
            }

            song.Artist = targetArtist;
            song.Album = targetAlbum;
            song.Genre = targetGenre;
            song.Playlists = playlists;
            song.Name = model.SongName;

            await _unitOfWork.SongRepository.AddAsync(song);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SongDetailedDTO>(song);
        }

        public async Task<SongDetailedDTO> UpdateAsync(string name, SongCreateAndUpdateDTO model) 
        {
            Song targetSong = await _unitOfWork.SongRepository.GetSingleWithPlaylistsByName(name);
            Album targetAlbum = await _unitOfWork.AlbumRepository.GetSingleByNameWithArtistsAsync(model.Album);
            Genre targetGenre = await _unitOfWork.GenreRepository.GetSingleByName(model.Genre);
            Artist targetArtist = targetAlbum.Artist;
            List<Playlist> playlists = new List<Playlist>();

            foreach (var playlist in model.Playlists)
            {
                Playlist targetPlaylist = await _unitOfWork.PlaylistRepository.GetSingleByNameAsync(playlist);
                playlists.Add(targetPlaylist);
            }

            targetSong.Artist = targetArtist;
            targetSong.Album = targetAlbum;
            targetSong.Genre = targetGenre;
            targetSong.Playlists = playlists;
            targetSong.Name = model.SongName;

            await _unitOfWork.SongRepository.UpdateAsync(targetSong);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SongDetailedDTO>(targetSong);
        }

        public async Task DeleteAsync(string name) 
        {
            Song targetSong = await _unitOfWork.SongRepository.GetSingleByName(name);

            await _unitOfWork.SongRepository.DeleteAsync(targetSong);
            await _unitOfWork.CompleteAsync();
        }


        public async Task<SongDetailedDTO> GetSingleDetailed(string songName)
        {
            var song = await _unitOfWork.SongRepository.GetSingleByNameWithVariablesAsync(songName);
            return _mapper.Map<SongDetailedDTO>(song);
        }

    }
}
