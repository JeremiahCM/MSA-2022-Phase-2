namespace AlbumReviewsAPI.Models
{
    public class AddAlbumReviewRequest
    {
        public string ArtistName { get; set; }
        public string AlbumName { get; set; }
        public string Review { get; set; }
    }
}
