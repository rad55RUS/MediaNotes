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
    /// <summary>
    /// Represents view model for favourites page view
    /// </summary>
    public class FavouritesViewModel : BaseViewModel_Movies
    {
        // Fields
        private bool welcomeMessage_IsVisible;
        //

        // Properties
        public bool WelcomeMessage_IsVisible
        {
            get { return welcomeMessage_IsVisible; }
            set { SetProperty(ref welcomeMessage_IsVisible, value); }
        }
        public Command OnAppearing_Command { get; }
        //

        // Constructors
        public FavouritesViewModel()
        {
            OnAppearing_Command = new Command(ExecuteLoadItemsCommand);
        }
        //

        // Methods
        async void ExecuteLoadItemsCommand()
        {
            OnAppearing();
            IsBusy = true;

            try
            {
                if (FavouritesDataStore.Count == 0)
                {
                    WelcomeMessage_IsVisible = true;
                }
                else
                {
                    WelcomeMessage_IsVisible = false;
                }

                Items.Clear();
                var items = await FavouritesDataStore.GetItemsAsync(true);
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