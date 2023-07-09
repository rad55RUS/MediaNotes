﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaNotes.Services
{
    public interface IDataStore<T>
    {
        int Count { get; }
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<bool> UpdateItemsAsync();
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
