using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class Pop : MusicTypes
    {
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
          
                using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.Pop.txt")))
                {
                  
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new Pop
                        {
                            Title = fields[1],
                            Author = fields[0]
                        };
                        MainPage.connection.Insert(songsData);
                        MainPage.connectionRestart.Insert(songsData);
                }
                   
                }
            
        }
        public override void Delete()
        {
            
        }
    }
}
