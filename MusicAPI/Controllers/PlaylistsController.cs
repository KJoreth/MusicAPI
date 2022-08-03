namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IUnitOfServices _unitOfServices;
        private readonly ILogger<PlaylistsController> _logger;
        public PlaylistsController(IUnitOfServices unitOfServices, ILogger<PlaylistsController> logger)
        {
            _unitOfServices = unitOfServices;
            _logger = logger;
        }

        /// <summary>
        /// Returns all Playlists in a simplified form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(statusCode:200)]
        public async Task<ActionResult<IEnumerable<PlaylistSimpleDTO>>> GetAllAsync()
        {
            var result = await _unitOfServices.PlaylistServices.GetAllAsync();
            _logger.LogInformation("All playlists were enlisted at {DT}",
                    DateTime.Now);
            return Ok(result);
        }

        /// <summary>
        /// Returns target Playlist with Song count
        /// </summary>
        /// <param name="playlistName"></param>
        /// <returns></returns>
        [HttpGet("{playlistName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult<PlaylistWithSongCountDTO>> GetSingleAsync(string playlistName)
        {
            var result = await _unitOfServices.PlaylistServices.GetSingleAsync(playlistName);

            if (result == null) 
            {
                _logger.LogInformation("Attempt to enlist non existing playlist at {DT}",
                    DateTime.Now);
                return NotFound($"Playlist doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            _logger.LogInformation("Playlist: {playlist} enlisted at {DT}",
                    playlistName ,DateTime.Now);
            return Ok(result);
        }

        /// <summary>
        /// Returns target Playlist in a detailed form
        /// </summary>
        /// <param name="playlistName"></param>
        /// <returns></returns>
        [HttpGet("{playlistName}/songs")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult<PlaylistDetailedDTO>> GetSingleDetailedAsync(string playlistName)
        {
            var result = await _unitOfServices.PlaylistServices.GetSingleDetailedAsync(playlistName);

            if (result == null)
            {
                _logger.LogInformation("Attempt to enlist non existing playlist with songs at {DT}",
                    DateTime.Now);
                return NotFound($"Playlist doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }


            _logger.LogInformation("Playlist: {playlist} enlisted with songs at {DT}",
                    playlistName, DateTime.Now);
            return Ok(result);
        }

        /// <summary>
        /// Returns target Song inside Playlist
        /// </summary>
        /// <param name="playlistName"></param>
        /// <param name="songName"></param>
        /// <returns></returns>
        [HttpGet("{playlistName}/songs/{songName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:400)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult<SongDetailedDTO>> GetSingleSongFromPlaylistAsync(string playlistName, string songName)
        {
            var targetPlaylist = await _unitOfServices.PlaylistServices.GetSingleDetailedAsync(playlistName);
            var targetSong = await _unitOfServices.SongServices.GetSingleAsync(songName);

            if (targetPlaylist == null || targetSong == null) 
            {
                _logger.LogInformation("Attempt to enlist non existing playlist or song at {DT}",
                    DateTime.Now);
                return NotFound($"Playlist or song doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }
                
            if (!targetPlaylist.Songs.Any(x => x.ToLower() == targetSong.SongName.ToLower())) 
            {
                _logger.LogInformation("Attempt to enlist song non existing inside playlist : {playlist} at {DT}",
                    playlistName, DateTime.Now);
                return BadRequest("This playlist doesn't contain this song");
            }
                

            var result = await _unitOfServices.SongServices.GetSingleDetailed(targetSong.SongName);
            _logger.LogInformation("Song: {song} was enlisted at {DT}",
                    songName, DateTime.Now);

            return Ok(result);

        }

        /// <summary>
        /// Creates new Playlist
        /// </summary>
        /// <param name="playlistName"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(statusCode:201)]
        [ProducesResponseType(statusCode:400)]
        public async Task<ActionResult> CreateNewAsync(string playlistName)
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning("Attempt to create playlist via invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Invalid data");
            }
                

            var result = await _unitOfServices.PlaylistServices.CreateNewAsync(playlistName);
            _logger.LogInformation("Playlist: {playlist} was created at {DT}",
                    playlistName ,DateTime.Now);
            return Created($"{Request.Scheme}://{Request.Host}{Request.Path}/{playlistName}", result);
        }

        /// <summary>
        /// Updates values of target Playlist
        /// </summary>
        /// <param name="playlistName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{playlistName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:400)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult> UpdateAsync(string playlistName, PlaylistSimpleDTO model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Failed playlist update because invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Invalid data");
            }

            var targetGenre = await _unitOfServices.PlaylistServices.GetSingleAsync(playlistName);
            if (targetGenre == null) 
            {
                _logger.LogInformation("Attempt to update non existing playlist at {DT}",
                    DateTime.Now);
                return NotFound($"Genre doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }
                

            await _unitOfServices.PlaylistServices.UpdateAsync(playlistName, model.PlaylistName);
            _logger.LogInformation("Playlist: {playlist} was updated at {DT}",
                    playlistName, DateTime.Now);
            return Ok();
        }

        /// <summary>
        /// Deletes target Playlist
        /// </summary>
        /// <param name="playlistName"></param>
        /// <returns></returns>
        [HttpDelete("{playlistName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult> DeleteAsync(string playlistName)
        {
            var targetGenre = await _unitOfServices.PlaylistServices.GetSingleAsync(playlistName);
            if (targetGenre == null) 
            {
                _logger.LogInformation("Attempt to delete non existing playlist at {DT}",
                    DateTime.Now);
                return NotFound($"Playlist doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }


            await _unitOfServices.PlaylistServices.DeleteAsync(playlistName);
            _logger.LogInformation("Playlist: {playlist} was deleted at {DT}",
                    playlistName, DateTime.Now);

            return Ok($"Playlist {playlistName} has beed deleted");
        }
    }
}
