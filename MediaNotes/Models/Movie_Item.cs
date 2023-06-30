using System;

namespace MediaNotes.Models
{
    /// <summary>
    /// Represents displayed movie item class
    /// </summary>
    public class Movie_Item
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Plot { get; set; }
        public double Rating { get; set; }
    }
}