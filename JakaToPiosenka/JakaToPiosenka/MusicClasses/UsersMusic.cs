using SQLite;
using System.IO;

namespace JakaToPiosenka.MusicClasses
{
    internal class UsersMusic : MUSICTYPES
    {
        // Właściwość FileName zwraca pustą wartość, ponieważ UsersMusic nie korzysta z pliku.
        public override string FileName => null;

        public override void Load()
        {
            // Tworzenie tabeli tylko dla UsersMusic
            connection.CreateTable<UsersMusic>();
            connectionRestart.CreateTable<UsersMusic>();
        }

        public override void Import()
        {
            // Usuwanie wszystkich danych z tabeli UsersMusic
            var tableMapping = connection.GetMapping<UsersMusic>();
            connection.Execute($"DELETE FROM {tableMapping.TableName}");

            var tableMappingRestart = connectionRestart.GetMapping<UsersMusic>();
            connectionRestart.Execute($"DELETE FROM {tableMappingRestart.TableName}");
        }

        public override void Delete()
        {
            // Możesz użyć Import() jako bazowego sposobu na usunięcie wszystkich rekordów
            Import();
        }
    }
}
