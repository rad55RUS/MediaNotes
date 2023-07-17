using MediaNotes.Models;
using MediaNotes.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaNotes.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel_Movies
    {
        // Fields
        private bool movieIsFavourite;
        private string movieFavouriteIcon;
        private string movieTitle;
        private string moviePoster;
        private string movieYear;
        private string movieRated;
        private string movieReleased;
        private string movieCountry;
        private string movieRuntime;
        private string movieGenre;  
        private string movieDirector;
        private string movieWriter;
        private string movieActors;
        private string movieBoxOffice;
        private string movieimdbRating;
        private string movieMetascore;
        private string moviePlot;
        //

        // Properties
        public string Id { get; set; }

        public Movie_Item CurrentMovie_Property { get => CurrentMovie.GetInstance(); }

        public string MovieTitle
        {
            get => CurrentMovie_Property.Title;
        }
        public string MoviePoster
        {
            get => CurrentMovie_Property.BigPoster;
        }
        #region Favourites
        public bool MovieIsFavourite
        {
            get
            {
                return movieIsFavourite;
            }
            set
            {
                movieIsFavourite = value;
                OnPropertyChanged(MovieFavouriteIcon);
            }
        }

        public string MovieFavouriteIcon
        {
            get
            {
                if (!MovieIsFavourite)
                {
                    return Movie_Item.AddIcon;
                }
                else
                {
                    return Movie_Item.AddedIcon;
                }
            }
            set
            {
                SetProperty(ref movieFavouriteIcon, value);
            }
        }
        #endregion

        #region Column data
        public string MovieYear
        {
            get => CurrentMovie_Property.Year;
        }
        public string MovieRated
        {
            get => CurrentMovie_Property.Rated;
        }
        public string MovieReleased 
        {
            get => CurrentMovie_Property.Released;
        }
        public string MovieCountry 
        {
            get => CurrentMovie_Property.Country;
        }
        public string MovieRuntime 
        {
            get => CurrentMovie_Property.Runtime;
        }
        public string MovieGenre 
        {
            get => CurrentMovie_Property.Genre;
        }
        public string MovieDirector
        {
            get => CurrentMovie_Property.Director;
        }
        public string MovieWriter 
        {
            get => CurrentMovie_Property.Writer;
        }
        public string MovieActors 
        {
            get => CurrentMovie_Property.Actors;
        }
        public string MovieBoxOffice 
        {
            get => CurrentMovie_Property.BoxOffice;
        }
        public string MovieimdbRating 
        {
            get => CurrentMovie_Property.imdbRating;
        }
        public string MovieMetascore
        {
            get => CurrentMovie_Property.Metascore;
        }
        #endregion

        public string MoviePlot
        {
            get => CurrentMovie_Property.Plot;
        }

        public new Command<Movie_Item> ItemFavouriteCommand { get; }
        //

        // Constructors
        public ItemDetailViewModel()
        {
            ItemFavouriteCommand = new Command<Movie_Item>(OnItemFavourite);

            LoadMovie();
        }
        //

        // Methods
        protected new void OnItemFavourite(Movie_Item item)
        {
            base.OnItemFavourite(item);

            MovieIsFavourite = !MovieIsFavourite;
            if (MovieIsFavourite)
            {
                MovieFavouriteIcon = Movie_Item.AddedIcon;
            }
            else
            {
                MovieFavouriteIcon = Movie_Item.AddIcon;
            }
        }

        public async void LoadMovie()
        {
            try
            {
                var movie = await CurrentMovie.GetInstanceAsync();
                Id = movie.Id;
                MovieIsFavourite = movie.IsFavourite;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        //
    }
}
