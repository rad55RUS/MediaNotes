// System libraries
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//

// Project libraries
using MediaNotes.Models;
//

namespace MediaNotes.Services
{
    /// <summary>
    /// Represents class for storing one Movie_Item
    /// </summary>
    public class CurrentMovie_SingleItem : ISingleItem
    {
        private Movie_Item instance;

        public CurrentMovie_SingleItem()
        { }

        #region ISingleItem realization
        /// <summary>
        /// Asynchonous ethod for updating class instance by other Movie_Item
        /// </summary>
        /// <param name="movie_Item"></param>
        /// <returns></returns>
        public async Task<bool> SetInstanceAsync(Movie_Item movie_Item)
        {
            instance = movie_Item;
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Method for getting current class instance
        /// </summary>
        /// <returns>Current Movie_Item class instance</returns>
        public Movie_Item GetInstance()
        {
            if (instance == null)
                instance = new Movie_Item();
            return instance;
        }

        /// <summary>
        /// Asynchronous method for getting current class instance
        /// </summary>
        /// <returns>Current Movie_Item class instance</returns>
        public async Task<Movie_Item> GetInstanceAsync()
        {
            if (instance == null)
                instance = new Movie_Item();
            return await Task.FromResult(instance);
        }
        #endregion
    }
}
