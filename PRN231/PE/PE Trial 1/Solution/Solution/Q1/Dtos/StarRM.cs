﻿namespace Q1.Dtos
{
    public class StarRM
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public bool? Male { get; set; }
        public DateTime? Dob { get; set; }
        public string? Description { get; set; }
        public string? Nationality { get; set; }
    }
}
