namespace MusicAPI.Services.Components
{
    public class AlbumServices : IAlbumServices
    {
        IAPIUnitOfWork _unitOfWork;
        IMapper _mapper;
        public AlbumServices(IAPIUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<AlbumSimpleDTO>> GetAllAsync()
        {
            List<Album> albums = await _unitOfWork.AlbumRepository.GetAllWithArtistsAndSongsAsync();
            List<AlbumSimpleDTO> albumSimpleDTOs = new();
            albums.ForEach(x => albumSimpleDTOs.Add(_mapper.Map<AlbumSimpleDTO>(x)));

            return albumSimpleDTOs;
        }

        public async Task<AlbumSimpleDTO> GetSingleAsync(string albumName)
        {
            Album targetAlbum = await _unitOfWork.AlbumRepository.GetSingleByNameWithArtistsAndSongsAsync(albumName);
            AlbumSimpleDTO albumSimpleDTO = _mapper.Map<AlbumSimpleDTO>(targetAlbum);

            return albumSimpleDTO;
        }

        public async Task<AlbumDetailedDTO> GetSingleDetailedAsync(string albumName)
        {
            Album targetAlbum = await _unitOfWork.AlbumRepository.GetSingleByNameWithArtistsAndSongsAsync(albumName);
            AlbumDetailedDTO albumDetailedDTO = _mapper.Map<AlbumDetailedDTO>(targetAlbum);

            return albumDetailedDTO;
        }

        public async Task CreateNewAsync(AlbumCreateAndUpdateDTO model)
        {
            var targetArtist = await _unitOfWork.ArtistRepository.GetSingleByNameAsync(model.ArtistName);

            Album albumToAdd = new() { Name = model.AlbumName, Artist = targetArtist };

            await _unitOfWork.AlbumRepository.AddAsync(albumToAdd);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(string albumName, AlbumCreateAndUpdateDTO model)
        {
            var targetAlbum = await _unitOfWork.AlbumRepository.GetSingleByNameAsync(albumName);
            targetAlbum.Name = model.AlbumName;
            var targetArtist = await _unitOfWork.ArtistRepository.GetSingleByNameAsync(model.ArtistName);
            targetAlbum.Artist = targetArtist;
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(string albumName)
        {
            var targetAlbum = await _unitOfWork.AlbumRepository.GetSingleByNameAsync(albumName);
            await _unitOfWork.AlbumRepository.DeleteAsync(targetAlbum);
            await _unitOfWork.CompleteAsync();
        }
    }
}
