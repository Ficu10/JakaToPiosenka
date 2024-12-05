using SQLite;
using System;
using System.IO;

public class Multiplayer
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public int GamesNumber { get; set; }

    public static SQLiteConnection connection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zgadula.db3"));

    public static void InitializeDatabase()
    {
        connection.CreateTable<Multiplayer>();
        connection.CreateTable<Settings>();
    }

    public static void AddPlayerToDatabase(Multiplayer player)
    {
        connection.Insert(player);
    }

    public static void RemovePlayerFromDatabase(Multiplayer player)
    {
        connection.Delete(player);
    }

    public static System.Collections.Generic.List<Multiplayer> GetAllPlayers()
    {
        return connection.Table<Multiplayer>().ToList();
    }

    public static void UpdatePlayer(Multiplayer player)
    {
        connection.Update(player);
    }

}

// Klasa do przechowywania ustawień aplikacji
public class Settings
{
    [PrimaryKey]
    public string Key { get; set; }
    public string Value { get; set; }

    public static void Set(string key, string value)
    {
        var existing = Multiplayer.connection.Find<Settings>(key);
        if (existing != null)
        {
            existing.Value = value;
            Multiplayer.connection.Update(existing);
        }
        else
        {
            Multiplayer.connection.Insert(new Settings { Key = key, Value = value });
        }
    }

    public static string Get(string key, string defaultValue = null)
    {
        var existing = Multiplayer.connection.Find<Settings>(key);
        return existing?.Value ?? defaultValue;
    }


}
