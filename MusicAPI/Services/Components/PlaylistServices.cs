namespace MusicAPI.Services.Components
{
    public class PlaylistServices : IPlaylistServices
    {
        IAPIUnitOfWork _unitOfWork;
        IMapper _mapper;
        public PlaylistServices(IAPIUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PlaylistSimpleDTO>> GetAllAsync()
        {
            List<Playlist> playlists = await _unitOfWork.PlaylistRepository.GetAllAsync();
            List<PlaylistSimpleDTO> playlistSimpleDTOs = new();
            playlists.ForEach(x => playlistSimpleDTOs.Add(_mapper.Map<PlaylistSimpleDTO>(x)));

            return playlistSimpleDTOs;
        }

        public async Task<PlaylistWithSongCountDTO> GetSingleAsync(string playlistName) 
        {
            string name = playlistName.Replace('-', ' ');
            Playlist targetPlaylist = await _unitOfWork.PlaylistRepository
                .GetSingleWithSongsByNameAsync(playlistName);

            PlaylistWithSongCountDTO playlistDetailedDTO = 
                _mapper.Map<PlaylistWithSongCountDTO>(targetPlaylist);

            return playlistDetailedDTO;
        }

        public async Task<PlaylistDetailedDTO> GetSingleDetailedAsync(string playlistName) 
        {
            string name = playlistName.Replace('-', ' ');
            Playlist targetPlaylist = await _unitOfWork.PlaylistRepository
                .GetSingleWithSongsByNameAsync(playlistName);

            PlaylistDetailedDTO playlistDetailedDTO =
                _mapper.Map<PlaylistDetailedDTO>(targetPlaylist);

            return playlistDetailedDTO;
        }

        public async Task<PlaylistSimpleDTO> CreateNewAsync(string playlistName) 
        {
            Playlist playlist = new() { Name = playlistName };
            await _unitOfWork.PlaylistRepository.AddAsync(playlist);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<PlaylistSimpleDTO>(playlist);
        }

        public async Task UpdateAsync(string playlistName, string newName) 
        {
            Playlist targetPlaylist = await _unitOfWork.PlaylistRepository.GetSingleByNameAsync(playlistName);
            targetPlaylist.Name = newName;
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(string playlistName) 
        {
            Playlist targetPlaylist = await _unitOfWork.PlaylistRepository.GetSingleByNameAsync(playlistName);
            await _unitOfWork.PlaylistRepository.DeleteAsync(targetPlaylist);
            await _unitOfWork.CompleteAsync();
        }

    }
}
