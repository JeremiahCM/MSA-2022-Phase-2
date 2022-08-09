using AlbumReviewsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbumReviewsAPI.Data
{
    public class AlbumReviewsAPIDbContext : DbContext
    {
        public AlbumReviewsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AlbumReview> AlbumReviews { get; set; }
    }
}
