using MediaReviewerServer.DTO;
using MediaReviewerServer.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaReviewerServer.Models
{
    public partial class MediaReviewerDbContext : DbContext
    {
        public User? GetUser(string email)
        {
            return this.Users.Where(u => u.Email == email).FirstOrDefault();
        }
        public List<Review>? GetReviewsByMovie(int movieId)
        {
            return this.Reviews.Where(r => r.MovieId == movieId).ToList();
        }

        public void SetMovieRating(int movieId, double rating)
        {
            var movie = this.Movies.Find(movieId);
            if (movie != null)
            {
                movie.Rating = rating;
                this.SaveChanges();
            }
        }

        public void SetReviewRating(int movieID, double rating)
        {
            var review = this.Reviews.FirstOrDefault(r => r.MovieId == movieID);
            if (review != null)
            {
                review.Rating = rating;
                this.SaveChanges();
            }
        }
        public void SetReviewDescription(int movieID, string description)
        {
            var review = this.Reviews.FirstOrDefault(r => r.MovieId == movieID);
            if (review != null)
            {
                review.Description = description;
                this.SaveChanges();
            }
        }
    }
}
