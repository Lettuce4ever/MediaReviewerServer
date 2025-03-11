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
    }
}
