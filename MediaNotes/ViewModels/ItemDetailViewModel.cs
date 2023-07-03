using MediaNotes.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaNotes.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        // Fields
        private string itemId;
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

        public string MovieTitle
        {
            get => movieTitle;
            set => SetProperty(ref movieTitle, value);
        }

        public string MoviePoster
        {
            get => moviePoster;
            set => SetProperty(ref moviePoster, value);
        }

        //// Column data
        public string MovieYear
        {
            get => movieYear;
            set => SetProperty(ref movieYear, value);
        }
        public string MovieRated
        {
            get => movieRated;
            set => SetProperty(ref movieRated, value);
        }
        public string MovieReleased 
        {
            get => movieReleased;
            set => SetProperty(ref movieReleased, value); 
        }
        public string MovieCountry 
        { 
            get => movieCountry;
            set => SetProperty(ref movieCountry, value); 
        }
        public string MovieRuntime 
        {
            get => movieRuntime;
            set => SetProperty(ref movieRuntime, value);
        }
        public string MovieGenre 
        {
            get => movieGenre; 
            set => SetProperty(ref movieGenre, value); 
        }
        public string MovieDirector
        {
            get => movieDirector; 
            set => SetProperty(ref movieDirector, value); 
        }
        public string MovieWriter 
        { 
            get => movieWriter; 
            set => SetProperty(ref movieWriter, value); 
        }
        public string MovieActors 
        { 
            get => movieActors; 
            set => SetProperty(ref movieActors, value);
        }
        public string MovieBoxOffice 
        { 
            get => movieBoxOffice; 
            set => SetProperty(ref movieBoxOffice, value);
        }
        public string MovieimdbRating 
        { 
            get => movieimdbRating;
            set => SetProperty(ref movieimdbRating, value);
        }
        public string MovieMetascore
        {
            get => movieMetascore;
            set => SetProperty(ref movieMetascore, value);
        }
        ////

        public string MoviePlot
        {
            get => moviePlot;
            set => SetProperty(ref moviePlot, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        //

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                MovieTitle = item.Title + ' ' + item.Year_Brackets;
                MoviePoster = item.BigPoster;
                MovieYear = item.Year;
                MovieRated = item.Rated;
                MovieReleased = item.Released;
                MovieCountry = item.Country;
                MovieRuntime = item.Runtime;
                MovieGenre = item.Genre;
                MovieDirector = item.Director;
                MovieWriter = item.Writer;
                MovieActors = item.Actors;
                MovieBoxOffice = item.BoxOffice;
                MovieimdbRating = item.imdbRating;
                MovieMetascore = item.Metascore;
                MoviePlot = item.Plot;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
