using MediaReviewerServer.Models;

namespace MediaReviewerServer.DTO
{
    public class ContentDTO
    {
        public int ContentId { get; set; }
        public string ContentName { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public int Length { get; set; }
        public string Description { get; set; } = null!;
        public double Rating { get; set; }
        public string Image { get; set; } = null!;
        public virtual ICollection<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();
        public virtual ICollection<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
    }
}
