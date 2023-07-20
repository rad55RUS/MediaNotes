using MediaNotes.Models;
using MediaNotes.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaNotes.ViewModels
{
    /// <summary>
    /// Represents view model for browsing specific item detail data
    /// </summary>
    public class ItemDetailViewModel : BaseViewModel_Movies
    {
        // Fields
        private bool movieIsFavourite;
        private bool isRatingVisibleFalseRated;
        private bool isRatingVisibleFalseNotRated;
        private bool isRatingVisibleTrue;
        private string movieFavouriteIcon;
        private string movieNotRatedIcon;
        private int movieUserRating;
        public IDataStore<Rating> RatingDataStore => DependencyService.Get<Rating_DataStore>();
        //

        // Properties
        public string Id { get; set; }

        public Movie_Item CurrentMovie_Property { get => CurrentMovie.GetInstance(); }
        public new ObservableCollection<Rating> Items { get; }

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

        #region Rating
        public int MovieUserRating
        {
            get => movieUserRating;
            set => SetProperty(ref movieUserRating, value);
        }

        public bool IsRatingVisibleFalseRated
        {
            get => isRatingVisibleFalseRated;
            set => SetProperty(ref isRatingVisibleFalseRated, value);
        }
        public bool IsRatingVisibleFalseNotRated
        {
            get => isRatingVisibleFalseNotRated;
            set => SetProperty(ref isRatingVisibleFalseNotRated, value);
        }
        public bool IsRatingVisibleTrue
        {
            get => isRatingVisibleTrue;
            set => SetProperty(ref isRatingVisibleTrue, value);
        }
        public string MovieNotRatedIcon
        {
            get
            {
                if (MovieUserRating == 0)
                {
                    return Movie_Item.SeenIcon;
                }
                else
                {
                    return Movie_Item.NotSeenIcon;
                }
            }
            set
            {
                SetProperty(ref movieNotRatedIcon, value);
            }
        }
        #endregion

        public string MoviePlot
        {
            get => CurrentMovie_Property.Plot;
        }

        public new Command<Movie_Item> ItemFavouriteCommand { get; }
        public Command<string> ItemRateCommand { get; }
        public Command OpenRatingCommand { get; }
        public Command CloseRatingCommand { get; }
        //

        // Constructors
        public ItemDetailViewModel()
        {
            Items = new ObservableCollection<Rating>();

            ItemRateCommand = new Command<string>(OnItemRated);
            ItemFavouriteCommand = new Command<Movie_Item>(OnItemFavourite);
            OpenRatingCommand = new Command(OnRatingOpened);
            CloseRatingCommand = new Command(OnRatingClosed);

            LoadMovie();
        }
        //

        // Methods
        #region Rating
        protected void OnRatingOpened()
        {
            IsRatingVisibleFalseRated = false;
            IsRatingVisibleFalseNotRated = false;
            IsRatingVisibleTrue = true;
        }

        protected void OnRatingClosed()
        {
            IsRatingVisibleTrue = false;

            if (MovieUserRating > 0)
            {
                IsRatingVisibleFalseRated =  true;
            }
            else
            {
                IsRatingVisibleFalseNotRated = true;

                if (MovieUserRating == 0)
                {
                    MovieNotRatedIcon = Movie_Item.SeenIcon;
                }
                else
                {
                    MovieNotRatedIcon = Movie_Item.NotSeenIcon;
                }
            }
        }

        protected async void OnItemRated(string newRating)
        {
            if (Items[1].Icon == Movie_Item.RatingEmptyIcon && newRating == "0")
            {
                newRating = "-1";
            }
            Items.Clear();
            for (int i = 0; i < 11; i++)
            {
                Rating rating = new Rating();
                rating.Id = i.ToString();
                if (i == 0)
                {
                    if (newRating == "-1")
                    {
                        rating.Icon = Movie_Item.NotSeenIcon;
                    }
                    else
                    {
                        rating.Icon = Movie_Item.SeenIcon;
                    }
                }
                else if (newRating == "-1")
                {
                    rating.Icon = Movie_Item.NotSeenEmptyIcon;
                }
                else if (i <= Convert.ToInt32(newRating))
                {
                    rating.Icon = Movie_Item.RatingIcon;
                }
                else
                {
                    rating.Icon = Movie_Item.RatingEmptyIcon;
                }
                await RatingDataStore.UpdateItemAsync(rating);

                Items.Add(await RatingDataStore.GetItemAsync(rating.Id));
            }
            MovieUserRating = Convert.ToInt32(newRating);

            OnItemRated(CurrentMovie_Property, newRating);
        }
        #endregion

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

                Items.Clear();
                for (int i = 0; i < 11; i++)
                {
                    Rating rating = new Rating();
                    rating.Id = i.ToString();
                    if (i == 0)
                    {
                        if (movie.UserRating == "-1")
                        {
                            rating.Icon = Movie_Item.NotSeenIcon;
                        }
                        else
                        {
                            rating.Icon = Movie_Item.SeenIcon;
                        }
                    }
                    else if (movie.UserRating == "-1")
                    {
                        rating.Icon = Movie_Item.NotSeenEmptyIcon;
                    }
                    else if (i <= Convert.ToInt32(movie.UserRating))
                    {
                        rating.Icon = Movie_Item.RatingIcon;
                    }
                    else
                    {
                        rating.Icon = Movie_Item.RatingEmptyIcon;
                    }
                    await RatingDataStore.UpdateItemAsync(rating);

                    Items.Add(await RatingDataStore.GetItemAsync(rating.Id));
                }

                MovieUserRating = Convert.ToInt32(movie.UserRating);

                if (MovieUserRating > 0)
                {
                    IsRatingVisibleFalseRated = true;
                }
                else
                {
                    IsRatingVisibleFalseNotRated = true;

                    if (MovieUserRating == 0)
                    {
                        MovieNotRatedIcon = Movie_Item.SeenIcon;
                    }
                    else
                    {
                        MovieNotRatedIcon = Movie_Item.NotSeenIcon;
                    }
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        //
    }
}
