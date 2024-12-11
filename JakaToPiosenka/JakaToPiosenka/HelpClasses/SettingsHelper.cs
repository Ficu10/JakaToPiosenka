using SQLite;
using System;
using System.IO;

namespace JakaToPiosenka.HelpClasses
{
    public static class SettingsHelper
    {
        private static readonly SQLiteConnection connection;

        // Konstruktor statyczny - inicjalizuje bazę danych
        static SettingsHelper()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Settings.db3");
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<Setting>();
        }

        // Pobierz wartość w postaci string z bazy danych
        public static string Get(string key, string defaultValue = null)
        {
            var setting = connection.Find<Setting>(key);
            return setting?.Value ?? defaultValue;
        }

        // Pobierz wartość liczbową z bazy danych
        public static int GetValue(string key, int defaultValue)
        {
            var setting = connection.Find<Setting>(key);
            if (setting != null && int.TryParse(setting.Value, out int result))
            {
                return result;
            }
            return defaultValue;
        }

        // Zapisz wartość w postaci string
        public static void Set(string key, string value)
        {
            var existingSetting = connection.Find<Setting>(key);
            if (existingSetting != null)
            {
                existingSetting.Value = value;
                connection.Update(existingSetting);
            }
            else
            {
                connection.Insert(new Setting { Key = key, Value = value });
            }
        }

        // Zapisz wartość liczbową
        public static void SetValue(string key, int value)
        {
            Set(key, value.ToString());
        }
    }

    // Klasa do reprezentowania ustawień
    public class Setting
    {
        [PrimaryKey]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
