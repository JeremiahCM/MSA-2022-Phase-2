namespace AlbumReviewsAPI.Models
{
    public class UpdateAlbumReviewRequest
    {
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public string Review { get; set; }
    }
}
