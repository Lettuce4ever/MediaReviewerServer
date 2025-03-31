namespace MediaReviewerServer.DTO
{
    public class GenreDTO
    {
        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;

        public GenreDTO()
        {

        }

        public GenreDTO(Models.Genre modelGenre)
        {
            this.GenreId = modelGenre.GenreId;
            this.GenreName = modelGenre.GenreName;
        }

        public Models.Genre GetModels()
        {
            Models.Genre modelGenre = new Models.Genre()
            {
                GenreId = this.GenreId,
                GenreName = this.GenreName,
            };
            return modelGenre;
        }
    }
}
