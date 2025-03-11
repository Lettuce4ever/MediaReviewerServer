namespace MediaReviewerServer.DTO
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }
        public int? UserId { get; set; }
        public int? MovieId { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; } = null!;
        public DateOnly ReviewDate { get; set; }
    }
}
