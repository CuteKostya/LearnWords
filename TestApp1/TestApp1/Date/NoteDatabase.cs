using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using TestApp1.Models;
using TestApp1.Services;

namespace Notes.Data
{
    public class NoteDatabase : IDataStore<DictionaryBo>
    {
        readonly SQLiteAsyncConnection _database;

        public NoteDatabase()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dbPath = Path.Combine(folderPath, "Notes1.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Item>().Wait();
            _database.CreateTableAsync<DictionaryBo>().Wait();
        }
        public Task<int> AddDictionaryAsync(DictionaryBo dictionary)
        {
            // Save a new note.
            return _database.InsertAsync(dictionary);
        }

        public async Task UpdateDictionaryAsync(DictionaryBo dictionary)
        {
            await _database.UpdateAsync(dictionary);
        }

        public async Task DeleteDictionaryAsync(int id)
        {
            // Delete a note.
            DictionaryBo oldDictionary = await _database.Table<DictionaryBo>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
            await _database.DeleteAsync(oldDictionary);
        }
        public Task<DictionaryBo> GetDictionaryAsync(int id)
        {
            return _database.Table<DictionaryBo>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> CountDictionaryAsync()
        {
            return _database.Table<DictionaryBo>()
                .CountAsync();
        }
        public async Task<IEnumerable<DictionaryBo>> GetDictionariesAsync(bool forceRefresh = false)
        {
            return await _database.Table<DictionaryBo>().ToListAsync();
        }
        public Task<int> AddItemAsync(Item item)
        {
            // Save a new note.
            return _database.InsertAsync(item);
        }
        public async Task UpdateItemAsync(Item item)
        {
            await _database.UpdateAsync(item);
        }
        public async Task DeleteItemAsync(int id)
        {
            // Delete a note.
            Item oldItem = await _database.Table<Item>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
            await _database.DeleteAsync(oldItem);
        }
        public Task<Item> GetItemAsync(int id)
        {
            return _database.Table<Item>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }
        public Task<int> CountItemAsync()
        {
            return _database.Table<Item>()
                .CountAsync();
        }
        public async Task<IEnumerable<Item>> GetItemsNotStudiedTenAsync(int id)
        {
            return await _database.Table<Item>()
                .Where(i => i.DictionaryId == id || i.Studied != true)
                .Take(10)
                .ToListAsync();
        }
        public async Task<IEnumerable<Item>> GetItemsNotStudiedAndBeingStudiedTenAsync(int id)
        {
            return await _database.Table<Item>()
                .Where(i => i.DictionaryId == id || i.Studied != true)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Choice>> GetChoiceAsync(bool forceRefresh = false)
        {
            return await _database.Table<Choice>().ToListAsync();
        }
    }
}