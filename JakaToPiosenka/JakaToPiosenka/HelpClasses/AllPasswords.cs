using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace JakaToPiosenka.HelpClasses
{
    abstract class AllPasswords
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Prompt { get; set; }

        public static SQLiteConnection connection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Zgadula.db3"));
        public static SQLiteConnection connectionRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ZgadulaRestart.db3"));

        public abstract string FileName { get; }

        public virtual async Task LoadAsync()
        {
            // Sprawdzanie i tworzenie tabel tylko raz
            if (!connection.TableMappings.Any(m => m.TableName == GetType().Name))
            {
                connection.CreateTable(GetType());
                connectionRestart.CreateTable(GetType());
            }

            var assembly = typeof(AllPasswords).GetTypeInfo().Assembly;
            var records = new List<AllPasswords>();

            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream($"JakaToPiosenka.TxtFiles.{FileName}.txt")))
            {
                string line;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    var fields = line.Split(';');
                    if (fields.Length < 2) continue; // Pomijanie błędnych linii

                    var data = Activator.CreateInstance(GetType()) as AllPasswords;
                    if (data != null)
                    {
                        data.Title = fields[1];
                        data.Prompt = fields[0];
                        records.Add(data);
                    }
                }
            }

            // Wstawianie wsadowe
            connection.InsertAll(records);
            connectionRestart.InsertAll(records);
        }

        public virtual async Task EnsureDataLoadedAsync()
        {
            if (!connection.GetTableInfo(GetType().Name).Any())
            {
                await LoadAsync();
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
