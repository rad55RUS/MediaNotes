using System;

namespace MediaNotes.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Plot { get; set; }
        public double Rating { get; set; }
    }
}