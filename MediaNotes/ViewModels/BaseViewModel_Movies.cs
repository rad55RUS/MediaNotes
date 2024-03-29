﻿// System libraries
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
//

// Project namespaces
using MediaNotes.Models;
using MediaNotes.Services;
using MediaNotes.Views;
//

namespace MediaNotes.ViewModels
{
    /// <summary>
    /// Represents base view model for using Movie_Item datastores
    /// </summary>
    public class BaseViewModel_Movies : BaseViewModel
    {
        // Fields
        public IDataStore<Movie_Item> FavouritesDataStore => DependencyService.Get<Favourites_DataStore>();
        public IDataStore<Movie_Item> MoviesDataStore => DependencyService.Get<Movies_DataStore>();
        public ISingleItem CurrentMovie => DependencyService.Get<CurrentMovie_SingleItem>();
        //

        // Properties
        public ObservableCollection<Movie_Item> Items { get; }

        //// Commands
        public Command<Movie_Item> ItemTappedCommand { get; }
        public Command<Movie_Item> ItemFavouriteCommand { get; }
        ////
        //

        // Constructors
        public BaseViewModel_Movies()
        {
            Items = new ObservableCollection<Movie_Item>();
            ItemTappedCommand = new Command<Movie_Item>(OnItemSelected);
            ItemFavouriteCommand = new Command<Movie_Item>(OnItemFavourite);
        }

        // Methods
        public void OnAppearing()
        {
            IsBusy = true;
        }

        protected async void OnItemRated(Movie_Item item, string rating)
        {
            if (rating == null || item == null)
                return;

            item.UserRating = rating;

            List<BaseMovie_Item> RatedItems = new List<BaseMovie_Item>(MediaNotes_Preferences.Rated_List);

            bool itemIsExist = false;
            for (int i = 0; i < RatedItems.Count; i++)
            {
                BaseMovie_Item ratedItem = RatedItems[i];
                if (ratedItem.Title == item.Title && ratedItem.Year == item.Year)
                {
                    if (rating == "-1")
                    {
                        RatedItems.Remove(ratedItem);
                        i--;
                    }
                    else
                    {
                        RatedItems[i].UserRating = rating;
                    }

                    itemIsExist = true;
                }
            }
            if (itemIsExist == false)
            {
                RatedItems.Add(item);
            }

            MediaNotes_Preferences.Rated_List = RatedItems;

            List<Movie_Item> Favourites = new List<Movie_Item>(MediaNotes_Preferences.Favourites_List);

            foreach (Movie_Item favourite in Favourites)
            {
                if (favourite.Title == item.Title && favourite.Year == item.Year)
                {
                    favourite.UserRating = rating;
                }
            }

            MediaNotes_Preferences.Favourites_List = Favourites;

            await FavouritesDataStore.UpdateItemsAsync();
            await MoviesDataStore.UpdateItemsAsync();
        }

        protected async void OnItemSelected(Movie_Item item)
        {
            if (item == null)
                return;

            await CurrentMovie.SetInstanceAsync(item);

            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}");
        }

        protected async void OnItemFavourite(Movie_Item item)
        {
            if (item == null)
                return;

            item.IsFavourite = !item.IsFavourite;
            if (item.IsFavourite)
            {
                List<Movie_Item> Favourites = new List<Movie_Item>(MediaNotes_Preferences.Favourites_List);

                Favourites.Add(item);

                MediaNotes_Preferences.Favourites_List = Favourites;

                item.FavouriteIcon = Movie_Item.AddedIcon;
            }
            else
            {
                List<Movie_Item> Favourites = new List<Movie_Item>(MediaNotes_Preferences.Favourites_List);

                for (int i = 0; i < Favourites.Count; i++)
                {
                    Movie_Item favourite = Favourites[i];
                    if (favourite.Title == item.Title && favourite.Year == item.Year)
                    {
                        Favourites.Remove(favourite);
                        i--;
                    }
                }

                MediaNotes_Preferences.Favourites_List = Favourites;

                item.FavouriteIcon = Movie_Item.AddIcon;
            }

            await FavouritesDataStore.UpdateItemsAsync();
            await MoviesDataStore.UpdateItemsAsync();
        }
        //
    }
}