using MediaNotes.Models;
using MediaNotes.Services;
using MediaNotes.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaNotes.ViewModels
{
    public class ItemsViewModel : BaseViewModel_Movies
    {
        // Properties
        public IDataStore<Movie_Item> MoviesDataStore => DependencyService.Get<Movies_DataStore>();
        //

        // Constructors
        public ItemsViewModel()
        {
            Task.Run(() => this.ExecuteLoadItemsCommand().Wait());
        }
        //

        // Methods
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                if (Items.Count == 0)
                {
                    var items = await MoviesDataStore.GetItemsAsync(true);
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        //
    }
}