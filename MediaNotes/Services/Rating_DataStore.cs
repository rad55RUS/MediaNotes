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

using MediaNotes.Models;

namespace MediaNotes.Services
{
    /// <summary>
    /// Represents DataStore for storing Rating items
    /// </summary>
    public class Rating_DataStore : IDataStore<Rating>
    {
        // Fields
        protected List<Rating> items = new List<Rating>();
        //

        // Constructors
        public Rating_DataStore()
        {
            Task.Run(() => this.LoadItemsAsync()).Wait();
        }
        //

        /// <summary>
        /// Asynchonous method for initializing items only with ids
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadItemsAsync()
        {
            for (int i = 0; i < 11; i++)
            {
                Rating rating = new Rating();
                rating.Id = i.ToString();
                items.Add(rating);
            }

            return await Task.FromResult(true);
        }

        #region IDataStore Realization
        /// <summary>
        /// Represents count of DataStore items
        /// </summary>
        public int Count
        {
            get => items.Count;
        }

        /// <summary>
        /// Asynchonous method for adding items into DataStore
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Rating item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchonous method for replacing existing item by new one
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Rating item)
        {
            var oldItem = items.Where((Rating arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchonous method for deleting existing item by using id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Rating arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchonous method. Does nothing. Should be used for updating current state of all items in DataStore
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateItemsAsync()
        {
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Asynchonous method for getting existing Rating item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Existing rating item</returns>
        public async Task<Rating> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Asynchonous method for getting enumerator with all existing Rating items
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Rating>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
        #endregion
    }
}