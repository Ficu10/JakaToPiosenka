using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class AllSongs : MusicTypes
    {
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.allSongs.txt")))
            {
                connection.CreateTable<AllSongs>();
                connectionRestart.CreateTable<AllSongs>();
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');
                    var songsData = new AllSongs
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
