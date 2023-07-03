// System libraries
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//

// Project libraries
using MediaNotes.Models;
//

// Nuget libraries
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Xamarin.Essentials;
using System.Net.Http;
using System.Diagnostics.Contracts;
using System.Net;
using System.Text;
//

namespace MediaNotes.Services
{
    public class MockDataStore : IDataStore<Movie_Item>
    {
        protected List<Movie_Item> items = new List<Movie_Item>();

        public MockDataStore()
        {
            Task.Run(() => this.LoadItemsAsync()).Wait();
        }

        /// <summary>
        /// Load items from json file asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadItemsAsync()
        {
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
            */
            #endregion
            
            #region Load all movies
            /**/
            using (var stream = await FileSystem.OpenAppPackageFileAsync("Movies.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    string fileContents = await reader.ReadToEndAsync();

                    itemsShort = JsonConvert.DeserializeObject<List<Movie_Item>>(fileContents);
                }
            }
            
            foreach (Movie_Item item in itemsShort)
            {
                string url = "http://www.omdbapi.com/?apikey=4e73d28b&t=" + item.Title.Replace(' ', '+') + "&y=" + item.Year + "&plot=full";

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
            }

            #endregion

            for (int i = 0; i < items.Count; i++)
            {
                items[i].Id = i.ToString();
                items[i].BoxOffice = items[i].BoxOffice.Replace(',', ' ');
                if (itemsShort.Count != 0)
                {
                    items[i].BigPoster = itemsShort[i].BigPoster;
                }
            }

            return await Task.FromResult(true);
        }
        /**/
        #region IDataStore Realization
        public async Task<bool> AddItemAsync(Movie_Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Movie_Item item)
        {
            var oldItem = items.Where((Movie_Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Movie_Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Movie_Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Movie_Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
        #endregion
    }
}