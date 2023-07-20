// System libraries
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Essentials;
using System.Net.Http;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
using Xamarin.Forms;
//

// Project libraries
using MediaNotes.Models;
//

// Nuget libraries
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Globalization;
//

namespace MediaNotes.Services
{
    /// <summary>
    /// Reprsents DataStore for storing common Movie_Items gained via API
    /// </summary>
    public class Movies_DataStore : BaseMovies_DataStore, IDataStore<Movie_Item>
    {
        // Constructors
        public Movies_DataStore()
        {
            Task.Run(() => this.LoadItemsAsync()).Wait();
        }
        //

        // Methods
        /// <summary>
        /// Asynchonous method for loading Movie_Items via API by using Movie.json file with only necessary movie data with getting extra data from saved cached data
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> LoadItemsAsync()
        {
            int current_page = 0;

            List<Movie_Item> itemsShort = new List<Movie_Item>();

            #region Load only one movie
            /*
            using (var stream = await FileSystem.OpenAppPackageFileAsync("Movie.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    string fileContents = await reader.ReadToEndAsync();

                    items.Add(JsonConvert.DeserializeObject<Movie_Item>(fileContents));
                }
            }
            /*
            #endregion

            #region Load all movies
            /**/
            using (var stream = await FileSystem.OpenAppPackageFileAsync("Movies.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    string fileContents = await reader.ReadToEndAsync();

                    itemsShort = JsonConvert.DeserializeObject<List<Movie_Item>>(fileContents);
                    page_amount = itemsShort.Count / 10;
                    if (page_amount * 10 > itemsShort.Count)
                    {
                        page_amount--;
                    }
                    page_amount++;
                }
            }

            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("page"))
            {
                current_page = (int)(Xamarin.Forms.Application.Current.Properties["page"] as int?);
                // do something with id
            }
            else
            {
                Xamarin.Forms.Application.Current.Properties["page"] = current_page;
            }
            for (int i = 10 * current_page; i < 10 * current_page + 10 && i < itemsShort.Count; i++)
            {
                Movie_Item item = itemsShort[i];

                await AddImdbMovie("4e73d28b", item.Title, item.Year);
            }
            /**/
            #endregion

            List<Movie_Item> favouritedItems = new List<Movie_Item>(MediaNotes_Preferences.Favourites_List);
            List<BaseMovie_Item> ratedItems = new List<BaseMovie_Item>(MediaNotes_Preferences.Rated_List);

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Id = i.ToString();
                items[i].BoxOffice = items[i].BoxOffice.Replace(',', ' ');
                if (itemsShort.Count != 0)
                {
                    items[i].BigPoster = itemsShort[i].BigPoster;
                }
                foreach (Movie_Item favouritedItem in favouritedItems)
                {
                    if (items[i].Title == favouritedItem.Title && items[i].Year == favouritedItem.Year)
                    {
                        items[i].IsFavourite = true;
                    }
                }
                foreach (BaseMovie_Item ratedItem in ratedItems)
                {
                    if (items[i].Title == ratedItem.Title && items[i].Year == ratedItem.Year)
                    {
                        items[i].UserRating = ratedItem.UserRating;
                    }
                }
            }

            return await Task.FromResult(true);
        }

        // Overrides
        /// <summary>
        /// Asynchonous method for updating Movie_Items that data in cached data have been changed 
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> UpdateItemsAsync()
        {
            List<Movie_Item> favouritedItems = new List<Movie_Item>(MediaNotes_Preferences.Favourites_List);

            if (favouritedItems.Count == 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].IsFavourite = false;
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    foreach (Movie_Item favouritedItem in favouritedItems)
                    {
                        if (items[i].Title == favouritedItem.Title && items[i].Year == favouritedItem.Year)
                        {
                            items[i].IsFavourite = true;
                        }
                    }
                }
            }

            List<BaseMovie_Item> ratedItems = new List<BaseMovie_Item>(MediaNotes_Preferences.Rated_List);

            if (ratedItems.Count == 0)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].UserRating = "-1";
                }
            }
            else
            {
                for (int i = 0; i < items.Count; i++)
                {
                    foreach (BaseMovie_Item ratedItem in ratedItems)
                    {
                        if (items[i].Title == ratedItem.Title && items[i].Year == ratedItem.Year)
                        {
                            items[i].UserRating = ratedItem.UserRating;
                        }
                    }
                }
            }

            return await Task.FromResult(true);
        }
        //

        // Privates
        /// <summary>
        /// Add movie to items from imdb using key, movie title and year
        /// </summary>
        /// <param name="key"></param>
        /// <param name="title"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private async Task<bool> AddImdbMovie(string key, string title, string year)
        {
            string url = "http://www.omdbapi.com/?apikey=" + key + "&t=" + title.Replace(' ', '+') + "&y=" + year + "&plot=full";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }
                    string data = readStream.ReadToEnd();
                    items.Add(JsonConvert.DeserializeObject<Movie_Item>(data));

                    response.Close();
                    readStream.Close();
                }
            }
            catch (Exception)
            {
                Debug.WriteLine(url + " response failed.");
            }

            return await Task.FromResult(true);
        }
        //
    }
    //
}