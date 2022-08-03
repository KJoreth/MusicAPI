namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfServices _unitOfServices;
        private readonly ILogger<GenresController> _logger;
        public GenresController(IUnitOfServices unitOfServices, ILogger<GenresController> logger)
        {
            _logger = logger;
            _unitOfServices = unitOfServices;
        }
        /// <summary>
        /// Returns all Genres in a simplified form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(statusCode:200)]
        public async Task<ActionResult<IEnumerable<GenreSimpleDTO>>> GetAllAsync()
        {
            var result = await _unitOfServices.GenreServices.GetAllAsync();
            _logger.LogInformation("All genres were enlisted at {DT}",
                    DateTime.Now);
            return Ok(result);
        }

        /// <summary>
        /// Returns single Genre with Song count
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns></returns>
        [HttpGet("{genreName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult<GenreWithSongCountDTO>> GetSingleAsync(string genreName)
        {
            var result = await _unitOfServices.GenreServices.GetSingleAsync(genreName);

            if (result == null) 
            {
                _logger.LogInformation("Attempt to enlist non existing genre at {DT}",
                    DateTime.Now);
                return NotFound($"Genre doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            _logger.LogInformation("Genre: {genre} was enlisted at {DT}",
                    genreName ,DateTime.Now);
            return Ok(result);
        }

        /// <summary>
        /// Returns target Genre in a detailed form with Song list
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns></returns>
        [HttpGet("{genreName}/songs")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult<GenreDetailedDTO>> GetSingleDetailedAsync(string genreName)
        {
            var result = await _unitOfServices.GenreServices.GetSingleDetailedAsync(genreName);

            if (result == null)
            {
                _logger.LogInformation("Attempt to enlist non existing genre at {DT}",
                    DateTime.Now);
                return NotFound($"Genre doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            _logger.LogInformation("Genre: {genre} was enlisted with songs at {DT}",
                    genreName, DateTime.Now);

            return Ok(result);
        }
        /// <summary>
        /// Returns target Song inside Album in a detailed form
        /// </summary>
        /// <param name="genreName"></param>
        /// <param name="songName"></param>
        /// <returns></returns>
        [HttpGet("{genreName}/songs/{songName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        [ProducesResponseType(statusCode:400)]
        public async Task<ActionResult<SongDetailedDTO>> GetSingleSongFromAlbumAsync(string genreName, string songName) 
        {
            var targetGenre = await _unitOfServices.GenreServices.GetSingleDetailedAsync(genreName);
            var targetSong = await _unitOfServices.SongServices.GetSingleAsync(songName);

            if (targetGenre == null || targetSong == null) 
            {
                _logger.LogInformation("Attempt to enlist non existing genre or song at {DT}",
                    DateTime.Now);
                return NotFound($"Genre or song doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            if (!targetGenre.Songs.Any(x => x.ToLower() == targetSong.SongName.ToLower())) 
            {
                _logger.LogInformation("Attempt to enlist song non existing inside album : {album} at {DT}",
                    genreName, DateTime.Now);
                return BadRequest("This genre doesn't contain this song");
            }
                

            var result = await _unitOfServices.SongServices.GetSingleDetailed(targetSong.SongName);
            _logger.LogInformation("Song: {song} was enlisted at {DT}",
                    songName, DateTime.Now);

            return Ok(result);

        }

        /// <summary>
        /// Creates new Genre
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(statusCode:201)]
        [ProducesResponseType(statusCode:400)]
        public async Task<ActionResult> CreateNewAsync(string genreName)
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning("Attempt to create genre via invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Invalid data");
            }
                
            var result = await _unitOfServices.GenreServices.CreateNewAsync(genreName);
            return Created($"{Request.Scheme}://{Request.Host}{Request.Path}/{genreName}", result);
        }

        /// <summary>
        /// Updates values of target Genre
        /// </summary>
        /// <param name="genreName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{genreName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:400)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult> UpdateAsync(string genreName, GenreSimpleDTO model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Failed genre update because invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Invalid data");
            }

            var targetGenre = await _unitOfServices.GenreServices.GetSingleAsync(genreName);
            if (targetGenre == null) 
            {
                _logger.LogInformation("Attempt to update non existing genre at {DT}",
                    DateTime.Now);
                return NotFound($"Genre doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }
                
            await _unitOfServices.GenreServices.UpdateAsync(genreName, model.GenreName);
            _logger.LogInformation("Genre: {genre} was updated at {DT}",
                    genreName ,DateTime.Now);
            return Ok(model);
        }

        /// <summary>
        /// Deletes target Genre
        /// </summary>
        /// <param name="genreName"></param>
        /// <returns></returns>
        [HttpDelete("{genreName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult> DeleteAsync(string genreName)
        {
            var targetGenre = await _unitOfServices.GenreServices.GetSingleAsync(genreName);
            if (targetGenre == null) 
            {
                _logger.LogInformation("Attempt to delete non existing genre at {DT}",
                    DateTime.Now);
                return NotFound($"Genre doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            await _unitOfServices.GenreServices.DeleteAsync(genreName);
            _logger.LogInformation("Genre: {genre} was deleted at {DT}",
                    genreName, DateTime.Now);

            return Ok($"Genre: {genreName} has beed deleted");
        }
    }
}
