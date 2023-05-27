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
    internal class Rap : MusicTypes
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.TxtFiles.Rap.txt")))
            {
                connection.CreateTable<Rap>();
                connectionRestart.CreateTable<Rap>();
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');
                    var songsData = new Rap
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

           
                        connection.DeleteAll<Rap>();
                        connectionRestart.DeleteAll<Rap>();
                      



        }
    }
}
