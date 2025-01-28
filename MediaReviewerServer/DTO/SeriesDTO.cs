namespace MediaReviewerServer.DTO
{
    public class SeriesDTO:ContentDTO
    {
        public string Trailer { get; set; } = null!;
        public string Creator { get; set; } = null!;
        public string Star { get; set; } = null!;
        public string Writer { get; set; } = null!;
        public bool MultiCreators { get; set; }
        public bool MultiStars { get; set; }
        public bool MultiWriters { get; set; }
    }
}
