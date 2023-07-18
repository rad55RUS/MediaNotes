using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediaNotes.Models
{
    /// <summary>
    /// Represents displayed movie item class
    /// </summary>
    public class Movie_Item : BaseMovie_Item, INotifyPropertyChanged
    {
        // Fields
        private bool isFavourite;
        private string favouriteIcon;
        private string runtime;
        //

        // Properties
        #region Detailed data
        public string Rated { get; set; }
        public string Released { get; set; }
        public string Country { get; set; }
        public string Runtime { get => runtime.Replace("min", Localization.Localization.MinuteShort_Locale); set => runtime = value; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Writer { get; set; }
        public string Actors { get; set; }
        public string BoxOffice { get; set; }
        public string Plot { get; set; }
        public string Metascore { get; set; }
        public string imdbRating { get; set; }
        #endregion

        #region Urls
        public string Poster { get; set; }
        public string BigPoster { get; set; }
        #endregion

        #region Constants
        public static string AddIcon { get => "icon_add.png"; }
        public static string AddedIcon { get => "icon_added.png"; }
        public static string RatingIcon { get => "icon_rating.png"; }
        public static string RatingEmptyIcon { get => "icon_ratingempty.png"; }
        public static string SeenIcon { get => "icon_seen.png"; }
        public static string NotSeenIcon { get => "icon_notseen.png"; }
        public static string NotSeenEmptyIcon { get => "icon_notseenempty.png"; }
        #endregion

        #region Defined data
        public string Year_Brackets
        {
            get => '(' + Year + ')';
        }
        #endregion

        #region Favourites
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
                    return AddIcon;
                }
                else
                {
                    return AddedIcon;
                }
            }
            set
            {
                SetProperty(ref favouriteIcon, value);
            }
        }
        #endregion

        #region Rating
        public bool IsRatingIconVisible
        {
            get
            { 
                if (Convert.ToInt32(UserRating) >= 0)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }
        public bool IsRatingTextVisible
        {
            get
            {
                if (Convert.ToInt32(UserRating) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public string RatingIconProperty
        {
            get
            {
                if (Convert.ToInt32(UserRating) > 0)
                {
                    return RatingIcon;
                }
                else
                {
                    return SeenIcon;
                }
            }
        }
        #endregion
        //
    }
}