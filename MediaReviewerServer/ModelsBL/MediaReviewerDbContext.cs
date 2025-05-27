using MediaReviewerServer.DTO;
using MediaReviewerServer.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace MediaReviewerServer.Models
{
    public partial class MediaReviewerDbContext : DbContext
    {
        public User? GetUser(string email)
        {
            return this.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User? GetUser(int id)
        {
            return this.Users.Where(u => u.UserId == id).FirstOrDefault();
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

        public void SetReviewRating(int movieID, int userID, double rating)
        {
            var review = this.Reviews.FirstOrDefault(r => r.MovieId == movieID&&r.UserId==userID);
            if (review != null)
            {
                review.Rating = rating;
                this.SaveChanges();
            }
        }
        public void SetReviewDescription(int movieID, int userID, string description)
        {
            var review = this.Reviews.FirstOrDefault(r => r.MovieId == movieID && r.UserId == userID);
            if (review != null)
            {
                review.Description = description;
                this.SaveChanges();
            }
        }

        public void SetReviewDate(int movieID, int userID, DateOnly date)
        {
            var review = this.Reviews.FirstOrDefault(r => r.MovieId == movieID && r.UserId == userID);
            if (review != null)
            {
                review.ReviewDate = date;
                this.SaveChanges();
            }
        }

        public void SetMovieName(int movieID, string name)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.MovieName = name;
                this.SaveChanges();
            }
        }

        public void SetMovieReleaseYear(int movieID, int releaseYear)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.ReleaseYear = releaseYear;
                this.SaveChanges();
            }
        }

        public void SetMovieLenth(int movieID, int length)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.Length = length;
                this.SaveChanges();
            }
        }
        public void SetMovieImage(int movieID, string image)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.Image = image;
                this.SaveChanges();
            }
        }
        public void SetMovieTrailer(int movieID, string trailer)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.Trailer = trailer;
                this.SaveChanges();
            }
        }
        public void SetMovieDescription(int movieID, string description)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.Description = description;
                this.SaveChanges();
            }
        }
        public void SetMovieDirector(int movieID, string director)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.Director = director;
                this.SaveChanges();
            }
        }
        public void SetMovieStar(int movieID, string star)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.Star = star;
                this.SaveChanges();
            }
        }

        public void SetMovieWriter(int movieID, string writer)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.Writer = writer;
                this.SaveChanges();
            }
        }

        public void SetMovieMultiDirectors(int movieID, bool multiDirectors)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.MultiDirectors = multiDirectors;
                this.SaveChanges();
            }
        }

        public void SetMovieMultiStars(int movieID, bool multiStars)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.MultiStars = multiStars;
                this.SaveChanges();
            }
        }

        public void SetMovieMultiWriters(int movieID, bool multiWriters)
        {
            var movie = this.Movies.Find(movieID);
            if (movie != null)
            {
                movie.MultiWriters = multiWriters;
                this.SaveChanges();
            }
        }

        public void SetMovieGenres(int movieID, List<GenreDTO> Genres)
        {
            var movie = this.Movies
                .Include(m => m.Genres)                              
                .FirstOrDefault(m => m.MovieId == movieID);

            if (movie != null)
            {
                movie.Genres.Clear();

                foreach (var genreDto in Genres)
                {
                    var existingGenre = this.Genres.Find(genreDto.GenreId); 
                    if (existingGenre != null)
                    {
                        movie.Genres.Add(existingGenre);         
                    }
                }
            }
        }

        public void RemoveUserReviews(int userId)
        {
            var reviews = this.Reviews.Where(r => r.UserId == userId).ToList();
            foreach (var review in reviews)
            {
                this.Reviews.Remove(review);
            }
            this.SaveChanges();
        }
    }
}
