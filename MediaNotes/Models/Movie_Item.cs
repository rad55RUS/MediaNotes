using System;

namespace MediaNotes.Models
{
    /// <summary>
    /// Represents displayed movie item class
    /// </summary>
    public class Movie_Item
    {
        // Preloaded data
        // Not displayed data
        public string Id { get; set; }
        //

        // Main data
        public string Title { get; set; }
        public string Year { get; set; }
        //

        // Detailed data
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Country { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string BoxOffice { get; set; }
        public string Plot { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        //

        // Urls
        public string Poster { get; set; }
        //
        //

        // Defined data
        public string Year_Brackets
        {
            get => '(' + Year + ')';
        }
        //
    }
}