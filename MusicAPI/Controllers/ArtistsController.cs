namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IUnitOfServices _unitOfServices;
        private readonly ILogger<ArtistsController> _logger;
        public ArtistsController(IUnitOfServices unitOfServices, ILogger<ArtistsController> logger)
        {
            _unitOfServices = unitOfServices;
            _logger = logger;
        }
        /// <summary>
        /// Returns all Artists in a simplified form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(statusCode:200)]
        public async Task<ActionResult<IEnumerable<ArtistSimpleDTO>>> GetAllAsync() 
        {
            var result = await _unitOfServices.ArtistServices.GetAllAsync();
            _logger.LogInformation("All Artists enlisted at {DT}",
                    DateTime.Now);
            return Ok(result);
        }
        /// <summary>
        /// Returns target Artists in a detailed form
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [HttpGet("{artistName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult<ArtistDetailedDTO>> GetSingleAsync(string artistName) 
        {
            var result = await _unitOfServices.ArtistServices.GetSingleDetailedAsync(artistName);

            if (result == null)
            {
                _logger.LogInformation("Attempt to enlist not existing artist at {DT}",
                    DateTime.Now);
                return NotFound($"Artist doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }

            _logger.LogInformation("Artist: {artist} enlisted at {DT}",
                    artistName, DateTime.Now);
            return Ok(result);
        }
        /// <summary>
        /// Creates new Artist
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(statusCode:201)]
        [ProducesResponseType(statusCode:400)]
        public async Task<ActionResult> CreateNewAsync(ArtistCreateAndUpdateDTO model) 
        {
            if (!ModelState.IsValid) 
            {
                _logger.LogWarning("Attempt to create artist via invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Invalid data");
            }
                     
            await _unitOfServices.ArtistServices.CreateNewAsync(model.ArtistName);

            _logger.LogInformation("Artist: {artist} has been created at {DT}",
                    model.ArtistName,DateTime.Now);
            return Created($"{Request.Scheme}://{Request.Host}{Request.Path}/{model.ArtistName}", model);
        }
        /// <summary>
        /// Updates target Artist
        /// </summary>
        /// <param name="artistName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("{artistName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:400)]
        public async Task<ActionResult> UpdateAsync(string artistName, ArtistCreateAndUpdateDTO model) 
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Failed artist update because invalid model at {DT}",
                    DateTime.Now);
                return BadRequest("Invalid data");
            }

            var targetArtist = await _unitOfServices.ArtistServices.GetSingleAsync(artistName);
            if(targetArtist == null) 
            {
                _logger.LogInformation("Attempt to update non existing artist at {DT}",
                    DateTime.Now);
                return NotFound($"Artist doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }
                

            await _unitOfServices.ArtistServices.UpdateAsync(artistName, model.ArtistName);
            _logger.LogInformation("Artist: {artist} was created at {DT}",
                    artistName, DateTime.Now);

            return Ok(model);
        }
        /// <summary>
        /// Deletes target Artist
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [HttpDelete("{artistName}")]
        [ProducesResponseType(statusCode:200)]
        [ProducesResponseType(statusCode:404)]
        public async Task<ActionResult> DeleteAsync(string artistName) 
        {
            var targetArtist = await _unitOfServices.ArtistServices.GetSingleAsync(artistName);
            if (targetArtist == null) 
            {
                _logger.LogInformation("Attempt to delete non existing artist at {DT}",
                    DateTime.Now);
                return NotFound($"Artist doesn't exist\nURL: {Request.Scheme}://{Request.Host}{Request.Path}");
            }
                

            await _unitOfServices.ArtistServices.DeleteAsync(artistName);

            _logger.LogInformation("Artist: {artist} was deleted at {DT}",
                    artistName ,DateTime.Now);
            return Ok($"Artist: {artistName} has been deleted");
        }

    }
}
