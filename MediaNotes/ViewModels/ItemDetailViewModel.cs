using MediaNotes.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MediaNotes.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string movieTitle;
        private string moviePlot;
        public string Id { get; set; }

        public string MovieTitle
        {
            get => movieTitle;
            set => SetProperty(ref movieTitle, value);
        }

        public string MoviePlot
        {
            get => moviePlot;
            set => SetProperty(ref moviePlot, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                MovieTitle = item.Title;
                MoviePlot = item.Plot;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
    }
}
