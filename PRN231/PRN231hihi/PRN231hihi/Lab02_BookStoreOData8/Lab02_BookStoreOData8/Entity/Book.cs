﻿using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class Book
    {
        [Key]
        public string? Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author {get; set; }
        public decimal Price { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public Press? Press { get; set; }
        public string? PressId { get; set; }
    }
}
