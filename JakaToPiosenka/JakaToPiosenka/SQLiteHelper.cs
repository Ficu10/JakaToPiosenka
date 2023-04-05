using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JakaToPiosenka
{
    public class SQLiteHelper
    {
        private readonly SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<SongsAndAuthors>();
        }

        public Task<int> CreateHistory(SongsAndAuthors songsAndAuthors)
        {
            return db.InsertAsync(songsAndAuthors);
        }
        public Task<List<SongsAndAuthors>> ReadSongsAndAuthors()
        {
            return db.Table<SongsAndAuthors>().ToListAsync();
        }

        public Task<int> DeleteSongsAndAuthors(SongsAndAuthors songsAndAuthors)
        {
            return db.DeleteAsync(songsAndAuthors);
        }

    }
}
