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
//

namespace MediaNotes.Services
{
    public class MockDataStore : IDataStore<Movie_Item>
    {
        protected List<Movie_Item> items;

        public MockDataStore()
        {
            Task.Run(() => this.LoadItemsAsync()).Wait();
            /*
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." }
            };
            */
        }

        /// <summary>
        /// Load items from json file asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadItemsAsync()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync("Movies.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    string fileContents = await reader.ReadToEndAsync();

                    items = JsonConvert.DeserializeObject<List<Movie_Item>>(fileContents);
                }
            }

            return await Task.FromResult(true);
        }

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