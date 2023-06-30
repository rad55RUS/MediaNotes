using MediaNotes.Models;
using MediaNotes.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaNotes.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Movie_Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}