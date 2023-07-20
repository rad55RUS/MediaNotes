using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MediaNotes.Models
{
    /// <summary>
    /// Represents class for storing id and icon for rating icons (CLASS SHOULD BE RENAMED)
    /// </summary>
    public class Rating : INotifyPropertyChanged
    {
        // Fields
        private string id;
        private string icon;
        //

        // Properties
        public string Id
        {
            get => id;
            set => id = value;
        }

        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
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
