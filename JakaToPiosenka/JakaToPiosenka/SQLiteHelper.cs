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
            db.CreateTableAsync<AllData>();

        }

        public Task<int> CreateHistory(AllData titlesAndPrompts)
        {
            return db.InsertAsync(titlesAndPrompts);
        }
        public Task<List<AllData>> ReadSongsAndAuthors()
        {
            return db.Table<AllData>().ToListAsync();
        }

        public Task<int> DeleteSongsAndAuthors(AllData titlesAndPrompts)
        {
            return db.DeleteAsync(titlesAndPrompts);
        }

    

    }
}
