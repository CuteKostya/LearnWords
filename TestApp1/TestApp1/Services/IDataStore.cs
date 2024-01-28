using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TestApp1.Models;

namespace TestApp1.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddDictionaryAsync(DictionaryBo dictionary);
        Task UpdateDictionaryAsync(DictionaryBo dictionary);
        Task DeleteDictionaryAsync(int id);
        Task<DictionaryBo> GetDictionaryAsync(int id);
        Task<IEnumerable<DictionaryBo>> GetDictionariesAsync(bool forceRefresh = false);
        Task<int> AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
        Task<Item> GetItemAsync(int id);
        Task<IEnumerable<Item>> GetItemsNotStudiedTenAsync(int id);
        Task<IEnumerable<Item>> GetItemsNotStudiedAndBeingStudiedTenAsync(int id); 
         Task<IEnumerable<Choice>> GetChoiceAsync(bool forceRefresh = false);
    }
}
