﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class FairyTales : MusicTypes
    {
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.FairyTales.txt")))
            {

                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');
                    var songsData = new FairyTales
                    {
                        Title = fields[1],
                        Author = fields[0]
                    };
                    MainPage.connection.Insert(songsData);
                    MainPage.connectionRestart.Insert(songsData);
                }

            }

        }
    }
}
