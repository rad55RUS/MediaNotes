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
    public class Favourites_DataStore : BaseMovies_DataStore, IDataStore<Movie_Item>
    {
        public Favourites_DataStore()
        {
            Task.Run(() => this.LoadItemsAsync()).Wait();
        }

        public override async Task<bool> LoadItemsAsync()
        {
            int current_page = 0;

            List<Movie_Item> favourites = new List<Movie_Item>(MediaNotes_Preferences.Favourites_List);

            page_amount = favourites.Count / 10;
            if (page_amount * 10 > favourites.Count)
            {
                page_amount--;
            }
                page_amount++;

            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("page_favourites"))
            {
                current_page = (int)(Xamarin.Forms.Application.Current.Properties["page_favourites"] as int?);
                // do something with id
            }
            else
            {
                Xamarin.Forms.Application.Current.Properties["page_favourites"] = current_page;
            }
            for (int i = 10 * current_page; i < 10 * current_page + 10 && i < favourites.Count; i++)
            {
                items.Add(favourites[i]);
            }

            return await Task.FromResult(true);
        }
    }
}