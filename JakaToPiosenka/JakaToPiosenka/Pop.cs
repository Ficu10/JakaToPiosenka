﻿using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class Pop : MusicTypes
    {
        public static List<Pop> popSongsList;
        public static List<Pop> popSongsListRestart;

        public static SQLiteConnection dbPop;
        public static SQLiteConnection dbPopRestart;


        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            dbPop = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PopDatabase.db3"));
            var dbPopRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PopDatabaseRestart.db3"));
            if (dbPopRestart != null)
            {
                using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.Pop.txt")))
                {
                    dbPopRestart.CreateTable<Pop>();
                    dbPop.CreateTable<Pop>();

                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new Pop
                        {
                            Title = fields[1],
                            Author = fields[0]
                        };
                        dbPopRestart.Insert(songsData);
                        dbPop.Insert(songsData);
                    }
                    popSongsList = dbPop.Table<Pop>().ToList();
                    popSongsListRestart = popSongsList;


                }
            }
        }
        public override void Delete()
        {
            
        }
    }
}
