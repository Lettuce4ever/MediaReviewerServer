namespace MediaReviewerServer.DTO
{
    public class MovieDTO:ContentDTO
    {
        public string Trailer { get; set; } = null!;
        public string Director { get; set; } = null!;
        public string Star { get; set; } = null!;
        public string Writer { get; set; } = null!;
        public bool MultiDirectors { get; set; }
        public bool MultiStars { get; set; }
        public bool MultiWriters { get; set; }
    }
}
