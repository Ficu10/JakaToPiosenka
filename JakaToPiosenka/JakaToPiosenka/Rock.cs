﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class Rock : MusicTypes
    {
        public static List<Rock> rockSongsList;
        public static List<Rock> rockSongsListRestart;

        public static SQLiteConnection dbRock;
        public static SQLiteConnection dbRockRestart;
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            dbRock = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RockDatabase.db3"));
            dbRockRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RockDatabaseRestart.db3"));
            if (dbRockRestart != null)
            {
                using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.allSongs.txt")))
                {
                    dbRockRestart.CreateTable<Rock>();  
                    dbRock.CreateTable<Rock>();  
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new Rock
                        {
                            Title = fields[1],
                            Author = fields[0]
                        };
                        dbRockRestart.Insert(songsData);
                        dbRock.Insert(songsData);
                    }
                    rockSongsList = dbRock.Table<Rock>().ToList();
                    rockSongsListRestart = rockSongsList;


                }
            }
        }
    }
}
