﻿using MediaNotes.Services;
using MediaNotes.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MediaNotes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<Movies_DataStore>();
            DependencyService.Register<Favourites_DataStore>();
            DependencyService.Register<Rating_DataStore>();
            DependencyService.Register<CurrentMovie_SingleItem>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
