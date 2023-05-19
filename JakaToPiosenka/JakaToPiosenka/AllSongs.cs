﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class AllSongs : MusicTypes
    {
        public static List<AllSongs> allSongsList;
        public static List<AllSongs> allSongsListRestart;

        public static SQLiteConnection dbAllSongs;
        public static SQLiteConnection dbAllSongsRestart;

        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            dbAllSongs = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AllSongsDatabase.db3"));
            dbAllSongsRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AllSongsDatabaseRestart.db3"));
            if (dbAllSongsRestart != null)
            {
                using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.allSongs.txt")))
                {
                    dbAllSongsRestart.CreateTable<AllSongs>();
                    dbAllSongs.CreateTable<AllSongs>();
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new AllSongs
                        {
                            Title = fields[1],
                            Author = fields[0]
                        };
                        dbAllSongsRestart.Insert(songsData);
                        dbAllSongs.Insert(songsData);
                    }
                    allSongsList = dbAllSongs.Table<AllSongs>().ToList();
                    allSongsListRestart = allSongsList;


                }
            }
        }


    }
}
