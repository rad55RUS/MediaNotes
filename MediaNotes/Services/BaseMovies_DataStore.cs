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
    /// <summary>
    /// Represents base abstract class for different movie datastores
    /// </summary>
    public abstract class BaseMovies_DataStore : IDataStore<Movie_Item>
    {
        // Fields
        protected int page_amount = 1;
        protected List<Movie_Item> items = new List<Movie_Item>();
        //

        /// <summary>
        /// Abstract method for load items asynchronously from any source
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> LoadItemsAsync();

        #region IDataStore Realization
        /// <summary>
        /// Represents count of items in a DataStore
        /// </summary>
        public int Count
        {
            get => items.Count;
        }

        /// <summary>
        /// Asynchonous method for adding specific item to a DataStore
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Movie_Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchonous method for updating existing item by new one
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Movie_Item item)
        {
            var oldItem = items.Where((Movie_Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchonous method for deleting existing item by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Movie_Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Abstract method for updating condition of all items
        /// </summary>
        /// <returns></returns>
        public abstract Task<bool> UpdateItemsAsync();

        /// <summary>
        /// Asynchonous method for getting existing item by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Movie_Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Asynchonous method for getting enumerator that contains all items from a DataStore 
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie_Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
        #endregion
    }
}