using SQLite;
using System;
using System.IO;
using System.Reflection;

namespace JakaToPiosenka
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
                connection.CreateTable(this.GetType());
                connectionRestart.CreateTable(this.GetType());
                string line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');
                    var data = Activator.CreateInstance(this.GetType()) as AllPasswords;
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
            var tableMapping = connection.GetMapping(this.GetType());
            connection.Execute($"DELETE FROM {tableMapping.TableName}");

            var tableMappingRestart = connectionRestart.GetMapping(this.GetType());
            connectionRestart.Execute($"DELETE FROM {tableMappingRestart.TableName}");
        }

        // Usuwanie danych (może być nadpisane w klasach pochodnych)
        public virtual void Delete()
        {
            var tableMapping = connection.GetMapping(this.GetType());
            connection.Execute($"DELETE FROM {tableMapping.TableName}");

            var tableMappingRestart = connectionRestart.GetMapping(this.GetType());
            connectionRestart.Execute($"DELETE FROM {tableMappingRestart.TableName}");
        }

        // Metoda, która może być używana do rozpoczęcia gry (do zaimplementowania w klasach pochodnych)
        public virtual void StartGame()
        {
            Console.WriteLine($"Starting game for {this.GetType().Name}");
        }
    }
}
