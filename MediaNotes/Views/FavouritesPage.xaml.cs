using MediaNotes.Models;
using MediaNotes.ViewModels;
using MediaNotes.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaNotes.Views
{
    public partial class FavouritesPage : ContentPage
    {
        FavouritesViewModel _viewModel;

        public FavouritesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new FavouritesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}