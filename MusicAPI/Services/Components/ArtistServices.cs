namespace MusicAPI.Services.Components
{
    public class ArtistServices : IArtistServices
    {
        private readonly IAPIUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ArtistServices(IAPIUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ArtistSimpleDTO>> GetAllAsync()
        {
            List<Artist> artists = await _unitOfWork.ArtistRepository.GetAllWithAlbumsAndSongsAsync();
            List<ArtistSimpleDTO> artistSimpleDTOs = new();
            artists.ForEach(x => artistSimpleDTOs.Add(_mapper.Map<ArtistSimpleDTO>(x)));

            return artistSimpleDTOs;
        }

        public async Task<ArtistSimpleDTO> GetSingleAsync(string name)
        {
            Artist targetArtist = await _unitOfWork.ArtistRepository
                    .GetSingleByNameWithAlbumsAndSongsAsync(name);

            ArtistSimpleDTO artistSimpleDTO = _mapper.Map<ArtistSimpleDTO>(targetArtist);

            return artistSimpleDTO;
        }

        public async Task<ArtistDetailedDTO> GetSingleDetailedAsync(string name)
        {
            Artist targetArtist = await _unitOfWork.ArtistRepository
                    .GetSingleByNameWithAlbumsAndSongsAsync(name);
            ArtistDetailedDTO artistDetailedDTO = _mapper.Map<ArtistDetailedDTO>(targetArtist);

            return artistDetailedDTO;
        }

        public async Task CreateNewAsync(string name)
        {
            Artist artist = new() { Name = name };
            await _unitOfWork.ArtistRepository.AddAsync(artist);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(string name, string newName)
        {
            Artist targetArtist = await _unitOfWork.ArtistRepository
                .GetSingleByNameAsync(name);

            targetArtist.Name = newName;

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(string name)
        {
            Artist targetArtist = await _unitOfWork.ArtistRepository
                .GetSingleByNameAsync(name);

            await _unitOfWork.ArtistRepository.DeleteAsync(targetArtist);
            await _unitOfWork.CompleteAsync();
        }
    }
}
