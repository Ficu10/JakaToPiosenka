using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using JakaToPiosenka;

namespace JakaToPiosenka
{
    public class SQLiteHelper
    {
        private readonly SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<NamesOfSongs>();
        }

        public Task<int> CreateHistory(NamesOfSongs history)
        {
            return db.InsertAsync(history);
        }
        public Task<List<NamesOfSongs>> ReadHistory()
        {
            return db.Table<NamesOfSongs>().ToListAsync();
        }

        public Task<int> DeleteHistory(NamesOfSongs history)
        {
            return db.DeleteAsync(history);
        }

    }
}