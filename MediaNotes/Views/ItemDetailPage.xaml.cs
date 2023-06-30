using MediaNotes.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MediaNotes.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}