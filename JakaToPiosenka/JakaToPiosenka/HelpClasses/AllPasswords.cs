using SQLite;
using System;
using System.IO;
using System.Reflection;

namespace JakaToPiosenka.HelpClasses
{
    abstract class AllPasswords
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Prompt { get; set; }

        // Bazy danych dla głównej i restartowanej gry
        public static SQLiteConnection connection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zgadula.db3"));
        public static SQLiteConnection connectionRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZgadulaRestart.db3"));

        // Nazwa pliku, którą definiują klasy pochodne
        public abstract string FileName { get; }

        // Ładowanie danych z pliku do bazy danych
        public virtual void Load()
        {
            var assembly = typeof(AllPasswords).GetTypeInfo().Assembly;

            // Użycie strumienia do odczytu pliku
            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream($"JakaToPiosenka.TxtFiles.{FileName}.txt")))
            {
                connection.CreateTable(GetType());
                connectionRestart.CreateTable(GetType());
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');

                    // Sprawdzenie, czy linia ma wystarczającą liczbę elementów
                    if (fields.Length < 2)
                    {
                        Console.WriteLine($"Nieprawidłowa linia: {line}");
                        continue; // Pomijamy linie z błędami
                    }

                    var data = Activator.CreateInstance(GetType()) as AllPasswords;
                    if (data != null)
                    {
                        data.Title = fields[1];
                        data.Prompt = fields[0];
                        connection.Insert(data);
                        connectionRestart.Insert(data);
                    }
                }
            }
        }

        // Import danych do bazy danych (usuwanie wszystkich istniejących rekordów)
        public virtual void Import()
        {
            var tableMapping = connection.GetMapping(GetType());
            connection.Execute($"DELETE FROM {tableMapping.TableName}");

            var tableMappingRestart = connectionRestart.GetMapping(GetType());
            connectionRestart.Execute($"DELETE FROM {tableMappingRestart.TableName}");
        }

        // Usuwanie danych (może być nadpisane w klasach pochodnych)
        public virtual void Delete()
        {
            var tableMapping = connection.GetMapping(GetType());
            connection.Execute($"DELETE FROM {tableMapping.TableName}");

            var tableMappingRestart = connectionRestart.GetMapping(GetType());
            connectionRestart.Execute($"DELETE FROM {tableMappingRestart.TableName}");
        }

        // Metoda, która może być używana do rozpoczęcia gry (do zaimplementowania w klasach pochodnych)
        public virtual void StartGame()
        {
            Console.WriteLine($"Starting game for {GetType().Name}");
        }

        // Tworzy tabelę i usuwa wszystkie dane
        public static void ClearTable<T>() where T : new()
        {
            connection.CreateTable<T>();
            connection.DeleteAll<T>();

            connectionRestart.CreateTable<T>();
            connectionRestart.DeleteAll<T>();
        }

        // Wstawia dane do tabel
        public static void InsertData<T>(T data) where T : new()
        {
            connection.Insert(data);
            connectionRestart.Insert(data);
        }
    }
}
