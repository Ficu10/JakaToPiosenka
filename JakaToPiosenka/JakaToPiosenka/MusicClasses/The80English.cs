﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xamarin.Essentials;

namespace JakaToPiosenka.MusicClasses
{
    internal class The80English : MusicTypes
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.TxtFiles.Rap.txt")))
            {
                connection.CreateTable<The80English>();
                connectionRestart.CreateTable<The80English>();
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');
                    var songsData = new The80English
                    {
                        Title = fields[1],
                        Author = fields[0]
                    };
                    connection.Insert(songsData);
                    connectionRestart.Insert(songsData);
                }

            }

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
                    connection.DeleteAll<The80English>();
                    connectionRestart.DeleteAll<The80English>();
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new The80English
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
