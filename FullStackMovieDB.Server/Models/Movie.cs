namespace FullStackMovieDB.Server.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public bool IsWatched { get; set; }
        public int? Rating { get; set; }
    }
}
