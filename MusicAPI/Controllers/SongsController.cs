namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly IUnitOfServices _unitOfServices;
        private readonly ILogger<SongsController> _logger;

        public SongsController(IUnitOfServices unitOfServices, 
            ILogger<SongsController> logger)
        {
            _unitOfServices = unitOfServices;
            _logger = logger;
        }

        /// <summary>
        /// Returns all Songs in a simplified form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(statusCode:200)]
        public async Task<ActionResult<List<SongSimpleDTO>>> GetAllAsync() 
        {
            var result = await _unitOfServices.SongServices.GetAllAsync();

            _logger.LogInformation("All songs enlisted at {DT}",
            DateTime.Now);

            return Ok(result);
        }

        /// <summary>
        /// Returns target Song in a detailed form
        /// </summary>
        /// <param name="songName"></param>
        /// <returns></returns>
        [HttpGet("{songName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult<SongDetailedDTO>> GetSingleAsync(string songName) 
        {
            var result = await _unitOfServices.SongServices.GetSingleAsync(songName);

            if (result == null)
            {
                _logger.LogInformation("Attempt to enlist not existing song: {name} at {DT}",
                    songName, DateTime.Now);

                return NotFound($"Song doesn't exist \nURL: {Request.Scheme}://{Request.Host}{Request.Path}");       
            }

            _logger.LogInformation("Song: {name} was enlisted at {DT}",
                    songName, DateTime.Now);

            return Ok(result);
        }

        /// <summary>
        /// Creates new Song
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(statusCode:201)]
        [ProducesResponseType(statusCode:400)]
        public async Task<ActionResult> CreateNewAsync(SongCreateAndUpdateDTO model)
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning("Failed song creation because invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Wrong input");
            }

            if (await CheckIfVariablesExists(model) != "Ok") 
            {
                _logger.LogInformation("Failed song creation because {error} at {DT}",
                    CheckIfVariablesExists(model), DateTime.Now);
                return BadRequest(CheckIfVariablesExists(model));
            }
                

            var result = await _unitOfServices.SongServices.CreateNewAsync(model);

            return Created($"{Request.Scheme}://{Request.Host}{Request.Path}", result);
        }

        
        /// <summary>
        /// Updates target song values
        /// </summary>
        /// <param name="songName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{songName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:400)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult<SongDetailedDTO>> UpdateAsync(string songName, SongCreateAndUpdateDTO model) 
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning("Failed song update because invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Wrong input");
            }

            if(await _unitOfServices.SongServices.GetSingleAsync(songName) == null) 
            {
                _logger.LogInformation("Failed song update because song doesn't exist at {DT}",
                    DateTime.Now);
                return NotFound($"Song doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }
                
            if (await CheckIfVariablesExists(model) != "Ok") 
            {
                _logger.LogInformation("Failed song update because {error} at {DT}",
                    CheckIfVariablesExists(model), DateTime.Now);
                return BadRequest(CheckIfVariablesExists(model));
            }

            var result = await _unitOfServices.SongServices.UpdateAsync(songName, model);
            _logger.LogInformation("Song: {song} was updated at {DT}",
                    songName, DateTime.Now);

            return Ok(result);
        }

        private async Task<string> CheckIfVariablesExists(SongCreateAndUpdateDTO model)
        {
            if (await _unitOfServices.AlbumServices.GetSingleAsync(model.Album) == null)
                return "Album doesn't exist";

            if (await _unitOfServices.GenreServices.GetSingleAsync(model.Genre) == null)
                return "Genre: doesn't exist";

            foreach (var playlist in model.Playlists)
                if (await _unitOfServices.PlaylistServices.GetSingleAsync(playlist) == null)
                    return $"Playlist: {playlist} doesn't exist";

            return "Ok";
        }

        /// <summary>
        /// Deletes target song
        /// </summary>
        /// <param name="songName"></param>
        /// <returns></returns>
        [HttpDelete("{songName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult> DeleteAsync(string songName) 
        {
            var targetSong = await _unitOfServices.SongServices.GetSingleAsync(songName);
            if (targetSong == null) 
            {
                _logger.LogInformation("Attempt to delete non existing song at {DT}",
                    DateTime.Now);
                return NotFound($"Song doesn't exists\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            _logger.LogInformation("Song: {song} was deleted at {DT}",
                    songName, DateTime.Now);
            await _unitOfServices.SongServices.DeleteAsync(songName);

            return Ok($"Song: {songName} has been deleted");
        }
    }
}
