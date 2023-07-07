using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediaNotes.Models
{
    /// <summary>
    /// Represents displayed movie item class
    /// </summary>
    public class Movie_Item : INotifyPropertyChanged
    {
        // Fields
        private bool isFavourite;
        private string favouriteIcon;
        //

        // Properties
        //// Preloaded data
        //// Not displayed data
        public string Id { get; set; }
        ////

        //// Main data
        public string Title { get; set; }
        public string Year { get; set; }
        ////

        //// Detailed data
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
        ////

        //// Urls
        public string Poster { get; set; }
        public string BigPoster { get; set; }
        ////

        //// Defined data
        public string Year_Brackets
        {
            get => '(' + Year + ')';
        }
        ////

        //// Favourites
        public bool IsFavourite
        {
            get
            {
                return isFavourite;
            }
            set
            { 
                isFavourite = value;
                OnPropertyChanged(FavouriteIcon);
            }
        }
        
        public string FavouriteIcon
        {
            get
            {
                if (!IsFavourite)
                {
                    return "icon_add.png";
                }
                else
                {
                    return "icon_added.png";
                }
            }
            set
            {
                SetProperty(ref favouriteIcon, value);
            }
        }
        ////
        //

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}