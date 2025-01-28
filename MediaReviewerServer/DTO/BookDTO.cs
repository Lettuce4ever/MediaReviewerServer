namespace MediaReviewerServer.DTO
{
    public class BookDTO:ContentDTO
    {
        public string Author { get; set; } = null!;
        public bool MultiAuthors { get; set; }
    }
}
