using System;
using System.Collections.Generic;
using MediaNotes.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MediaNotes
{
    static class MediaNotes_Preferences
    {
        public static List<Movie_Item> Favourites_List
        {
            get
            {
                if (Preferences.Get(nameof(Favourites_List), null) != null)
                {
                    var List = Deserialize<List<Movie_Item>>(Preferences.Get(nameof(Favourites_List), null));
                    return List ?? new List<Movie_Item>();
                }
                else
                {
                    return new List<Movie_Item>(); 
                }
            }
            set
            {
                var List = Serialize(value);
                Preferences.Set(nameof(Favourites_List), List);
            }
        }

        static T Deserialize<T>(string serializedObject) => JsonConvert.DeserializeObject<T>(serializedObject);

        static string Serialize<T>(T objectToSerialize) => JsonConvert.SerializeObject(objectToSerialize);
    }
}