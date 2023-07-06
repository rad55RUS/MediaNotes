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
    public class FavouritesViewModel : BaseViewModel
    {
        // Fields
        private Movie_Item _selectedItem;
        //

        // Properties
        public IDataStore<Movie_Item> DataStore => DependencyService.Get<Favourites_DataStore>();
        public ObservableCollection<Movie_Item> Items { get; }

        //// Commands
        public Command<Movie_Item> ItemTapped { get; }
        ////
        //

        public FavouritesViewModel()
        {
            Items = new ObservableCollection<Movie_Item>();
            ItemTapped = new Command<Movie_Item>(OnItemSelected);

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
            SelectedItem = null;
        }

        public Movie_Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Movie_Item item)
        {
            if (item == null)
                return;

            await CurrentMovie.SetInstanceAsync(item);

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}");
        }
    }
}