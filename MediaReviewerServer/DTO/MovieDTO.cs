using MediaReviewerServer.Models;

namespace MediaReviewerServer.DTO
{
    public class MovieDTO
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int Length { get; set; }
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public string Image { get; set; } = null!;
        public string Trailer { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Star { get; set; } = null!;
        public string Writer { get; set; } = null!;
        public bool MultiDirectors { get; set; }
        public bool MultiStars { get; set; }
        public bool MultiWriters { get; set; }
        public virtual ICollection<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
        public virtual ICollection<GenreDTO> Genres { get; set; } = new List<GenreDTO>();

        public MovieDTO() 
        {

        }

        public MovieDTO(Models.Movie modelMovie)
        {
            this.MovieId = modelMovie.MovieId;
            this.MovieName = modelMovie.MovieName;
            this.ReleaseYear = modelMovie.ReleaseYear;
            this.Length = modelMovie.Length;
            this.Description = modelMovie.Description;
            this.Rating = modelMovie.Rating;
            this.Image = modelMovie.Image;
            this.Trailer = modelMovie.Trailer;
            this.Director = modelMovie.Director;
            this.Star = modelMovie.Star;
            this.Writer = modelMovie.Writer;
            this.MultiDirectors = modelMovie.MultiDirectors;
            this.MultiStars = modelMovie.MultiStars;
            this.MultiWriters = modelMovie.MultiWriters;
            this.Reviews = new List<ReviewDTO>();
            if (modelMovie.Reviews != null)
            {
                foreach (Review r in modelMovie.Reviews)
                    this.Reviews.Add(new ReviewDTO(r));
            }
            this.Genres = new List<GenreDTO>();
            if(modelMovie.Genres != null)
            {
                foreach (Genre g in modelMovie.Genres)
                    this.Genres.Add(new GenreDTO(g));
            }
        }

        public Models.Movie GetModels()
        {
            List<Models.Genre> genres = new List<Genre>();
            foreach(GenreDTO g in this.Genres)
            {
                genres.Add(new Genre()
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName
                });
            }
            Models.Movie modelMovie = new Models.Movie()
            {
                MovieId = this.MovieId,
                MovieName = this.MovieName,
                ReleaseYear = this.ReleaseYear,
                Length = this.Length,
                Description = this.Description,
                Rating = this.Rating,
                Image = this.Image,
                Trailer = this.Trailer,
                Director = this.Director,
                Star = this.Star,
                Writer = this.Writer,
                MultiDirectors = this.MultiDirectors,
                MultiStars = this.MultiStars,
                MultiWriters = this.MultiWriters,
                Genres = genres


            };
            return modelMovie;
        }
    }
}
