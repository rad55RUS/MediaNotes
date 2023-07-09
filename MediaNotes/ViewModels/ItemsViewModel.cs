using MediaNotes.Models;
using MediaNotes.Services;
using MediaNotes.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace MediaNotes.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        // Properties
        public IDataStore<Movie_Item> DataStore => DependencyService.Get<Movies_DataStore>();
        public ObservableCollection<Movie_Item> Items { get; }

        //// Commands
        public Command<Movie_Item> ItemTappedCommand { get; }
        public Command<Movie_Item> ItemFavouriteCommand { get; }
        ////
        //

        public ItemsViewModel()
        {
            Items = new ObservableCollection<Movie_Item>();
            ItemTappedCommand = new Command<Movie_Item>(OnItemSelected);
            ItemFavouriteCommand = new Command<Movie_Item>(OnItemFavourite);

            Task.Run(() => this.ExecuteLoadItemsCommand().Wait());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                if (Items.Count == 0)
                {
                    var items = await DataStore.GetItemsAsync(true);
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

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async void OnItemSelected(Movie_Item item)
        {
            if (item == null)
                return;

            await CurrentMovie.SetInstanceAsync(item);

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}");
        }

        void OnItemFavourite(Movie_Item item)
        {
            if (item == null)
                return;

            item.IsFavourite = !item.IsFavourite;
            if (item.IsFavourite)
            {
                List<Movie_Item> Favourites = new List<Movie_Item>(MediaNotes_Preferences.Favourites_List);

                Favourites.Add(item);

                MediaNotes_Preferences.Favourites_List = Favourites;

                item.FavouriteIcon = "icon_added.png";
            }
            else
            {
                List<Movie_Item> Favourites = new List<Movie_Item>(MediaNotes_Preferences.Favourites_List);

                Favourites.Remove(item);

                MediaNotes_Preferences.Favourites_List = Favourites;

                item.FavouriteIcon = "icon_add.png";
            }
        }
    }
}