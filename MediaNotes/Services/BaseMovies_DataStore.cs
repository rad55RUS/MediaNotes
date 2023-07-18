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
//

namespace MediaNotes.Services
{
    public abstract class BaseMovies_DataStore : IDataStore<Movie_Item>
    {
        // Fields
        protected int page_amount = 1;
        protected List<Movie_Item> items = new List<Movie_Item>();
        //
        public abstract Task<bool> LoadItemsAsync();

        #region IDataStore Realization
        public int Count
        {
            get => items.Count;
        }

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

        public abstract Task<bool> UpdateItemsAsync();

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
