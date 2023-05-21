using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class Rock : MusicTypes
    {
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.Rock.txt")))
            {
                connection.CreateTable<Rock>();
                connectionRestart.CreateTable<Rock>();
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');
                    var songsData = new Rock
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
