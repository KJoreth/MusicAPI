namespace MusicAPI.Services.Components
{
    public class GenreServices : IGenreServices
    {
        IAPIUnitOfWork _unitOfWork;
        IMapper _mapper;
        public GenreServices(IAPIUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<GenreSimpleDTO>> GetAllAsync()
        {
            List<Genre> genres = await _unitOfWork.GenreRepository.GetAllAsync();
            List<GenreSimpleDTO> genresSimpleDTOs = new();
            genres.ForEach(x => genresSimpleDTOs.Add(_mapper.Map<GenreSimpleDTO>(x)));

            return genresSimpleDTOs;
        }

        public async Task<GenreWithSongCountDTO> GetSingleAsync(string name) 
        {
            var targetGenre = await _unitOfWork.GenreRepository.GetSingleWithSongsByName(name);

            return _mapper.Map<GenreWithSongCountDTO>(targetGenre);
        }

        public async Task<GenreDetailedDTO> GetSingleDetailedAsync(string name) 
        {
            var targetGenre = await _unitOfWork.GenreRepository.GetSingleWithSongsByName(name);

            return _mapper.Map<GenreDetailedDTO>(targetGenre);        
        }

        public async Task<GenreSimpleDTO> CreateNewAsync(string name) 
        {
            Genre genre = new() { Name = name };
            await _unitOfWork.GenreRepository.AddAsync(genre);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GenreSimpleDTO>(genre);
        }

        public async Task UpdateAsync(string name, string newName) 
        {
            Genre targetGenre = await _unitOfWork.GenreRepository.GetSingleByName(name);
            targetGenre.Name = newName;
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(string name) 
        {
            Genre targetGenre = await _unitOfWork.GenreRepository.GetSingleByName(name);
            await _unitOfWork.GenreRepository.DeleteAsync(targetGenre);
            await _unitOfWork.CompleteAsync();
        }
    }
}
