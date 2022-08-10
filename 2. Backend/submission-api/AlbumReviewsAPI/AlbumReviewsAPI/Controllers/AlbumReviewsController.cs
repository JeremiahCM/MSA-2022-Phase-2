using AlbumReviewsAPI.Data;
using AlbumReviewsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace AlbumReviewsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumReviewsController : ControllerBase
    {
        private readonly AlbumReviewsAPIDbContext dbContext;
        private readonly IDeezerService deezerService;

        public AlbumReviewsController(AlbumReviewsAPIDbContext dbContext, IDeezerService deezerService)
        {
            this.dbContext = dbContext;
            this.deezerService = deezerService;
        }

        // Retrieves all album reviews
        [HttpGet]
        public async Task<IActionResult> GetAllAlbumReviews()
        {
            return Ok(await dbContext.AlbumReviews.ToListAsync());
        }

        /* READ operation
         * Search for an existing album review in the database by id
         */
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetAlbumReview([FromRoute] Guid id)
        {
            var albumReview = await dbContext.AlbumReviews.FindAsync(id);

            if (albumReview == null)
            {
                return NotFound();
            }

            return Ok(albumReview);
        }

        /* CREATE operation
         * Make a new album review for the database
         */
        [HttpPost]
        public async Task<IActionResult> AddAlbumReview(AddAlbumReviewRequest addAlbumReviewRequest)
        {
            var detailsContent = await deezerService.GetAlbumFromDeezer(addAlbumReviewRequest.ArtistName, addAlbumReviewRequest.AlbumName);

            var detailsJson = JObject.Parse(detailsContent);
            var detailsReleaseDate = (string)detailsJson["release_date"];
            var detailsGenre = (string)detailsJson["genres"]["data"][0]["name"];
            var detailsNumTracks = (string)detailsJson["nb_tracks"];

            var albumReview = new AlbumReview()
            {
                Id = Guid.NewGuid(),
                ArtistName = addAlbumReviewRequest.ArtistName,
                AlbumName = addAlbumReviewRequest.AlbumName,
                ReleaseDate = detailsReleaseDate,
                Genre = detailsGenre,
                NumTracks = detailsNumTracks,
                Review = addAlbumReviewRequest.Review
            };

            await dbContext.AlbumReviews.AddAsync(albumReview);
            await dbContext.SaveChangesAsync();

            return Ok(albumReview);
        }

        /* Update operation
         * Checks if ID exists first before attempting album review update in the database
         */
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateAlbumReview([FromRoute] Guid id, UpdateAlbumReviewRequest updateAlbumReviewRequest)
        {
            var albumReview = await dbContext.AlbumReviews.FindAsync(id);

            if (albumReview == null)
            {
                return NotFound();
            }

            albumReview.ArtistName = updateAlbumReviewRequest.ArtistName;
            albumReview.AlbumName = updateAlbumReviewRequest.AlbumName;
            albumReview.Review = updateAlbumReviewRequest.Review;

            await dbContext.SaveChangesAsync();

            return Ok(albumReview);
        }

        /* DELETE operation
         * Check if ID exists first before attempting album review deletion from the database
         * Passing delete album review back in case user would like to use it for something
         */
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteAlbumReview(Guid id)
        {
            var albumReview = await dbContext.AlbumReviews.FindAsync(id);

            if (albumReview == null)
            {
                return NotFound();
            }

            dbContext.Remove(albumReview);
            await dbContext.SaveChangesAsync();
            return Ok(albumReview);
        }
    }
}
