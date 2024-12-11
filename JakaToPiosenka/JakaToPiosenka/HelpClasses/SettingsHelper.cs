using SQLite;
using System;
using System.IO;

namespace JakaToPiosenka.HelpClasses
{
    public class SettingsHelper
    {
        [PrimaryKey]
        public string Key { get; set; } // Klucz identyfikujący ustawienie
        public string Value { get; set; } // Wartość ustawienia

        private static readonly SQLiteConnection connection;

        // Konstruktor statyczny do zainicjalizowania połączenia z bazą danych
        static SettingsHelper()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Settings.db3");
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<SettingsHelper>(); // Upewnia się, że tabela istnieje
        }

        // Dodaj lub nadpisz wartość w bazie danych
        public static void Set(string key, string value)
        {
            var existing = connection.Find<SettingsHelper>(key);
            if (existing != null)
            {
                existing.Value = value;
                connection.Update(existing);
            }
            else
            {
                connection.Insert(new SettingsHelper { Key = key, Value = value });
            }
        }

        // Pobierz wartość z bazy danych
        public static string Get(string key, string defaultValue = null)
        {
            var existing = connection.Find<SettingsHelper>(key);
            return existing?.Value ?? defaultValue;
        }

        // Pobierz wartość w formie liczbowej
        public static int GetValue(string key, int defaultValue)
        {
            var existing = connection.Find<SettingsHelper>(key);
            return existing != null ? int.Parse(existing.Value) : defaultValue;
        }

        // Zapisz wartość liczbową
        public static void SetValue(string key, int value)
        {
            Set(key, value.ToString());
        }
    }
}
