﻿using MediaNotes.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaNotes.Services
{
    public interface ISingleItem
    {
        Task<bool> SetInstanceAsync(Movie_Item movie_Item);
        Task<Movie_Item> GetInstanceAsync();
    }
}
