using Q1.Models;

namespace Q1.Dtos
{
    public class MovieRM
    {
        public int id { get; set; }
        public string title { get; set; } = null!;
        public DateTime? releaseDate { get; set; }
        public string? releaseYear { get; set; }
        public string? description { get; set; }
        public string language { get; set; } = null!;
        public int? producerId { get; set; }
        public int? directorId { get; set; }

        public string? directorName { get; set; }
        public string? producerName { get; set; }

        public virtual ICollection<GenerRM> genres { get; set; }
        public virtual ICollection<StarRM> stars { get; set; }
    }
}
