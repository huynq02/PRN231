namespace Q1.Dtos
{
    public class DirectRm
    {
        
            public int id { get; set; }
            public string fullName { get; set; } = null!;
            public string gender { get; set; }
            public DateTime dob { get; set; }
            public string dobString { get; set; }
            public string nationality { get; set; } = null!;
            public string description { get; set; } = null!;

            public virtual ICollection<MovieRM> movies { get; set; }
        
    }
}
