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
        public static List<FairyTales> fairyTalesSongsList;
        public static List<FairyTales> fairyTalesSongsListRestart;

        public static SQLiteConnection dbFairyTales;
        public static SQLiteConnection dbFairyTalesRestart;
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            dbFairyTales = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FairyTalesDatabase.db3"));
            dbFairyTalesRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FairyTalesDatabaseRestart.db3"));
            if (dbFairyTalesRestart != null)
            {
                using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.FairyTales.txt")))
                {
                    dbFairyTalesRestart.CreateTable<FairyTales>();
                    dbFairyTales.CreateTable<FairyTales>();
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new FairyTales
                        {
                            Title = fields[1],
                            Author = fields[0]
                        };
                        dbFairyTalesRestart.Insert(songsData);
                        dbFairyTales.Insert(songsData);
                    }
                    fairyTalesSongsList = dbFairyTales.Table<FairyTales>().ToList();
                    fairyTalesSongsListRestart = fairyTalesSongsList;


                }
            }
        }
    }
}
