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
    public class FavouritesViewModel : BaseViewModel_Movies
    {
        public Command OnAppearing_Command { get; }

        // Constructors
        public FavouritesViewModel()
        {
            OnAppearing_Command = new Command(ExecuteLoadItemsCommand);
        }
        //

        // Methods
        async void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                if (Items.Count != FavouritesDataStore.Count)
                {
                    Items.Clear();
                    var items = await FavouritesDataStore.GetItemsAsync(true);
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