namespace Spring2026_Project3_mbvasquez.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? ImbdLink { get; set; }
        public string? Genre { get; set; }
        public int YearOfRelease { get; set; }
        public byte[]? Poster { get; set; }

    }
}
