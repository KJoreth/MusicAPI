namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IUnitOfServices _unitOfServices;
        private readonly ILogger<AlbumsController> _logger;
        public AlbumsController(IUnitOfServices unitOfServices, ILogger<AlbumsController> logger)
        {
            _unitOfServices = unitOfServices;
            _logger = logger;
        }
        /// <summary>
        /// Returns all Albums in a simplified form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(statusCode:200)]
        public async Task<ActionResult<IEnumerable<AlbumSimpleDTO>>> GetAllAsync()
        {
            var result = await _unitOfServices.AlbumServices.GetAllAsync();
            _logger.LogInformation("All Albums enlisted at {DT}",
                    DateTime.Now);
            return Ok(result);
        }

        /// <summary>
        /// Returns target album in a simplified form
        /// </summary>
        /// <param name="albumName"></param>
        /// <returns></returns>
        [HttpGet("{albumName}")]
        public async Task<ActionResult<AlbumSimpleDTO>> GetSingleAsync(string albumName)
        {
            var result = await _unitOfServices.AlbumServices.GetSingleAsync(albumName);

            if (result == null) 
            {
                _logger.LogInformation("Attempt to enlist non existing album: {album} at {DT}",
                    albumName, DateTime.Now);
                return NotFound($"Album doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            _logger.LogInformation("Album: {album} enlisted at {DT}",
                    albumName, DateTime.Now);
            return Ok(result);
        }

        /// <summary>
        /// Returns target alubm in a detailed form with a list of songs
        /// </summary>
        /// <param name="albumName"></param>
        /// <returns></returns>
        [HttpGet("{albumName}/songs")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult<AlbumDetailedDTO>> GetSingleDetailedAsync(string albumName)
        {
            AlbumDetailedDTO result = await _unitOfServices.AlbumServices
                .GetSingleDetailedAsync(albumName);

            if (result == null) 
            {
                _logger.LogInformation("Attempt to enlist non existing album: {album} at {DT}",
                    albumName, DateTime.Now);
                return NotFound($"Album doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            _logger.LogInformation("Album: {album} enlisted with songs at {DT}",
                    albumName, DateTime.Now);
            return Ok(result);
        }

        /// <summary>
        /// Returns target song inside album
        /// </summary>
        /// <param name="albumName"></param>
        /// <param name="songName"></param>
        /// <returns></returns>
        [HttpGet("{albumName}/songs/{songName}")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<ActionResult<SongDetailedDTO>> GetSongFromAlbumAsync
            (string albumName, string songName)
        {

            var targetAlbum = await _unitOfServices.AlbumServices.GetSingleDetailedAsync(albumName);
            var targetSong = await _unitOfServices.SongServices.GetSingleAsync(songName);

            if (targetAlbum == null || targetSong == null) 
            {
                _logger.LogInformation("Attempt to enlist non existing album or song at {DT}",
                    DateTime.Now);
                return NotFound($"Album or Song doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }
                
            if (!targetAlbum.Songs.Any(x => x.ToLower() == targetSong.SongName.ToLower())) 
            {
                _logger.LogInformation("Attempt to enlist song non existing inside album : {album} at {DT}",
                    albumName, DateTime.Now);
                return BadRequest("This album doesn't contain this song");
            }
                

            var result = await _unitOfServices.SongServices.GetSingleAsync(targetSong.SongName);
            _logger.LogInformation("Song: {song} enlisted at {DT}",
                    songName, DateTime.Now);
            return Ok(result);

        }
        /// <summary>
        /// Creates new album
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(statusCode: 201)]
        [ProducesResponseType(statusCode: 400)]
        public async Task<ActionResult> CreateNewAsync(AlbumCreateAndUpdateDTO model)
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning("Attemt to create new album via invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Invalid data");
            }
                

            var targetArtist = await _unitOfServices.ArtistServices.GetSingleAsync(model.ArtistName);

            if (targetArtist == null) 
            {
                _logger.LogInformation("Attemt to create new album for non existing Artist: {artist} at {DT}",
                    model.ArtistName, DateTime.Now);
                return BadRequest("Artist doesn't exist");
            }
                

            await _unitOfServices.AlbumServices.CreateNewAsync(model);

            _logger.LogInformation("Created new Album: {album} at {DT}", model.AlbumName, DateTime.Now);

            return Created($"{Request.Scheme}://{Request.Host}{Request.Path}/{model.AlbumName}", model);
        }
        /// <summary>
        /// Updates values of the target album
        /// </summary>
        /// <param name="albumName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{albumName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:400)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult> UpdateAsync(string albumName, AlbumCreateAndUpdateDTO model)
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning("Failed album update because invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Invalid data");
            }
                

            var targetAlbum = await _unitOfServices.AlbumServices.GetSingleAsync(albumName);

            if (targetAlbum == null) 
            {
                _logger.LogInformation("Attemt to update non existing Album at {DT}",
                    DateTime.Now);
                return NotFound($"Album doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }
                

            var targetArtist = await _unitOfServices.ArtistServices.GetSingleAsync(albumName);

            if (targetArtist == null) 
            {
                _logger.LogInformation("Attemt to update Album with non existing Artist: {artist} at {DT}",
                    model.ArtistName, DateTime.Now);
                return BadRequest("Artist doesn't exist");
            }
                

            await _unitOfServices.AlbumServices.UpdateAsync(albumName, model);
            _logger.LogInformation("Album: {album} updated at {DT}",
                    model.AlbumName, DateTime.Now);

            return Ok(model);
        }
        /// <summary>
        /// Deletes target album
        /// </summary>
        /// <param name="albumName"></param>
        /// <returns></returns>
        [HttpDelete("{albumName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult> DeleteAsync(string albumName)
        {
            var targetAlbum = await _unitOfServices.AlbumServices.GetSingleAsync(albumName);

            if (targetAlbum == null)
            {
                _logger.LogInformation("Attemt to delete non existing album at {DT}",
                    DateTime.Now);
                return NotFound($"Album doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

           await  _unitOfServices.AlbumServices.DeleteAsync(albumName);
            _logger.LogInformation("Album: {album} was deleted at {DT}",
                        albumName, DateTime.Now);
            return Ok($"Album: {albumName} has been deleted");
        }


    }
}
