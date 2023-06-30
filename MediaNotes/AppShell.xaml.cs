using MediaNotes.ViewModels;
using MediaNotes.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MediaNotes
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
        }

    }
}
