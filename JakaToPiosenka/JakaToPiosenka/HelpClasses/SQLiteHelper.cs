using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JakaToPiosenka.HelpClasses
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
        public Task<int> SaveSetting(string key, string value)
        {
            // Sprawdź, czy ustawienie już istnieje
            var existingSetting = db.Table<AllData>().Where(s => s.Title == key).FirstOrDefaultAsync();
            return existingSetting.ContinueWith(task =>
            {
                if (task.Result != null)
                {
                    task.Result.Prompt = value; // Aktualizuj wartość
                    return db.UpdateAsync(task.Result);
                }
                else
                {
                    return db.InsertAsync(new AllData { Title = key, Prompt = value }); // Wstaw nowe ustawienie
                }
            }).Unwrap();
        }

        public Task<string> GetSetting(string key, string defaultValue = null)
        {
            return db.Table<AllData>().Where(s => s.Title == key).FirstOrDefaultAsync()
                .ContinueWith(task =>
                {
                    return task.Result?.Prompt ?? defaultValue;
                });
        }

        public async Task InitializeSettings(Dictionary<string, string> defaultSettings)
        {
            foreach (var setting in defaultSettings)
            {
                await SaveSetting(setting.Key, setting.Value);
            }
        }



    }
}
