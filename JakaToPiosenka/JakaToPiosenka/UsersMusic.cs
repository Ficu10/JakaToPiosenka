using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;

namespace JakaToPiosenka
{
    internal class UsersMusic : MusicTypes
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public override void Load()
        {
            connection.CreateTable<UsersMusic>();
            connectionRestart.CreateTable<UsersMusic>();

        }

        public async override void Import(string text)
        {

            List<string> filePaths = new List<string>();
            var allFiles = Directory.GetFiles(FileSystem.AppDataDirectory, text + "JakaToPiosenka.txt", SearchOption.AllDirectories);

            filePaths = allFiles.ToList();
            string filePath = allFiles.ToList()[0];
            if (filePath != null)
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    connection.DeleteAll<UsersMusic>();
                    connectionRestart.DeleteAll<UsersMusic>();
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new UsersMusic
                        {
                            Title = fields[1],
                            Author = fields[0]
                        };
                        connection.Insert(songsData);
                        connectionRestart.Insert(songsData);
                    }

                }
            }
           

        }
    }
}
