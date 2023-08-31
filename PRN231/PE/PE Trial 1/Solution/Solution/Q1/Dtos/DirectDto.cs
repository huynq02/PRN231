namespace Q1.Dtos
{
    public class DirectDto
    {
        public int id { get; set; }
        public string fullName { get; set; } = null!;
        public string gender { get; set; }
        public DateTime dob { get; set; }
        public string dobString { get; set; }
        public string nationality { get; set; } = null!;
        public string description { get; set; } = null!;
    }
}
