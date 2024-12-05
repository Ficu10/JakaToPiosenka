using SQLite;
using System;
using System.IO;

public class SettingsHelper
{
    [PrimaryKey]
    public string Key { get; set; } // Klucz identyfikujący ustawienie
    public string Value { get; set; } // Wartość ustawienia

    public static SQLiteConnection connection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zgadula.db3"));

    // Metoda do inicjalizacji bazy danych
    public static void InitializeDatabase()
    {
        connection.CreateTable<SettingsHelper>();
    }

    // Dodaj lub nadpisz wartość w bazie danych
    public static void AddNewValue(SettingsHelper setting, int value)
    {
        // Sprawdź, czy istnieje wpis z danym kluczem
        var existingSetting = connection.Find<SettingsHelper>(setting.Key);

        if (existingSetting != null)
        {
            // Jeśli istnieje, zaktualizuj jego wartość
            existingSetting.Value = value.ToString();
            connection.Update(existingSetting);
        }
        else
        {
            // Jeśli nie istnieje, wstaw nowy wpis
            connection.Insert(new SettingsHelper { Key = setting.Key, Value = value.ToString() });
        }
    }

    // Pobierz wartość z bazy danych
    public static int GetValue(string key, int defaultValue)
    {
        var existingSetting = connection.Find<SettingsHelper>(key);
        return existingSetting != null ? int.Parse(existingSetting.Value) : defaultValue;
    }
}
