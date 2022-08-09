namespace AlbumReviewsAPI.Models
{
    public class AlbumReview
    {
        public Guid Id { get; set; }
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public string Review { get; set; }
    }
}
