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
    public class CurrentMovie_SingleItem : ISingleItem
    {
        private Movie_Item instance;

        public CurrentMovie_SingleItem()
        { }

        public async Task<bool> SetInstanceAsync(Movie_Item movie_Item)
        {
            instance = movie_Item;
            return await Task.FromResult(true);
        }

        public Movie_Item GetInstance()
        {
            if (instance == null)
                instance = new Movie_Item();
            return instance;
        }

        public async Task<Movie_Item> GetInstanceAsync()
        {
            if (instance == null)
                instance = new Movie_Item();
            return await Task.FromResult(instance);
        }
    }
}
