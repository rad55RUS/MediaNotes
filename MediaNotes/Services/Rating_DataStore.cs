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
    public class Rating_DataStore : IDataStore<Rating>
    {
        protected List<Rating> items = new List<Rating>();

        public Rating_DataStore()
        {
            Task.Run(() => this.LoadItemsAsync()).Wait();
        }

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
        public int Count
        {
            get => items.Count;
        }

        public async Task<bool> AddItemAsync(Rating item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Rating item)
        {
            var oldItem = items.Where((Rating arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Rating arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemsAsync()
        {
            return await Task.FromResult(true);
        }

        public async Task<Rating> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Rating>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
        #endregion
    }
}
