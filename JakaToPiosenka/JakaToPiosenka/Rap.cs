﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class Rap : MusicTypes
    {
        public static List<Rap> rapSongsList;
        public static List<Rap> rapSongsListRestart;

        public static SQLiteConnection dbRap;
        public static SQLiteConnection dbRapRestart;

        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            dbRap = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RapDatabase.db3"));
            dbRapRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RapDatabaseRestart.db3"));
            if (dbRapRestart != null)
            {
                using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.Rap.txt")))
                {
                    dbRapRestart.CreateTable<Rap>();
                    dbRap.CreateTable<Rap>();
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new Rap
                        {
                            Title = fields[1],
                            Author = fields[0]
                        };
                        dbRapRestart.Insert(songsData);
                        dbRap.Insert(songsData);
                    }
                    rapSongsList = dbRap.Table<Rap>().ToList();
                    rapSongsListRestart = rapSongsList;


                }
            }
        }
    }
}
