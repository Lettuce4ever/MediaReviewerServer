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

        public ReviewDTO()
        {

        }

        public ReviewDTO(Models.Review modelReview)
        {
            this.ReviewId = modelReview.ReviewId;
            this.UserId = modelReview.UserId;
            this.MovieId = modelReview.MovieId;
            this.Rating = modelReview.Rating;
            this.Description = modelReview.Description;
            this.ReviewDate = modelReview.ReviewDate;
        }

        public Models.Review GetModels()
        {
            Models.Review modelReview = new Models.Review()
            {
                ReviewId = this.ReviewId,
                UserId = this.UserId,
                MovieId = this.MovieId,
                Rating = this.Rating,
                Description = this.Description,
                ReviewDate = this.ReviewDate
            };
            return modelReview;
        }
    }
}
