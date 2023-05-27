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
    class Youtube : MusicTypes
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.TxtFiles.Youtube.txt")))
            {
                connection.CreateTable<Youtube>();
                connectionRestart.CreateTable<Youtube>();
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');
                    var songsData = new Youtube
                    {
                        Title = fields[1],
                        Author = fields[0]
                    };
                    connection.Insert(songsData);
                    connectionRestart.Insert(songsData);
                }

            }

        }

        public async override void Import()
        {

           
                    connection.DeleteAll<Youtube>();
                    connectionRestart.DeleteAll<Youtube>();
                    

        }
    }
}
