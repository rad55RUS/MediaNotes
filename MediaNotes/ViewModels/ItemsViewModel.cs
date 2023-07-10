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
        public Command OnAppearing_Command { get; }
        //

        // Constructors
        public ItemsViewModel()
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
                Items.Clear();
                var items = await MoviesDataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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