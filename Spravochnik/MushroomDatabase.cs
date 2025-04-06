using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;


namespace Spravochnik
{
    public class MushroomDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public MushroomDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Mushroom>().Wait();
        }

        public Task<List<Mushroom>> GetItemsAsync() => _database.Table<Mushroom>().ToListAsync();
        public Task<int> SaveItemAsync(Mushroom item) => item.Id == 0 ? _database.InsertAsync(item) : _database.UpdateAsync(item);
        public Task<int> DeleteItemAsync(Mushroom item) => _database.DeleteAsync(item);
    }
}
