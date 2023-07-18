using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MediaNotes.Models
{
    /// <summary>
    /// Represents displayed movie item class
    /// </summary>
    public class BaseMovie_Item : INotifyPropertyChanged
    {
        // Fields
        private string userRating = "-1";
        //

        // Properties
        //// Preloaded data
        //// Not displayed data
        public string Id { get; set; }
        ////

        //// Main data
        public string Title { get; set; }
        public string Year { get; set; }
        ////

        public string UserRating 
        { 
            get => userRating;
            set => SetProperty(ref userRating, value);
        }

        //

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}