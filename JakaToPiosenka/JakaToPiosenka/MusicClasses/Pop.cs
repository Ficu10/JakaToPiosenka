using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;

namespace JakaToPiosenka.MusicClasses
{
    internal class Pop : MusicTypes
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            using (var streamReader = new StreamReader(assembly.GetManifestResourceStream("JakaToPiosenka.TxtFiles.Pop.txt")))
            {
                connection.CreateTable<Pop>();
                connectionRestart.CreateTable<Pop>();
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var fields = line.Split(';');
                    var songsData = new Pop
                    {
                        Title = fields[1],
                        Author = fields[0]
                    };
                    connection.Insert(songsData);
                    connectionRestart.Insert(songsData);
                }

            }

        }

        public async override void Import(string text)
        {

            List<string> filePaths = new List<string>();
            var allFiles = Directory.GetFiles(FileSystem.AppDataDirectory, text + "JakaToPiosenka.txt", SearchOption.AllDirectories);

            filePaths = allFiles.ToList();
            string filePath = allFiles.ToList()[0];
            if (filePath != null)
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    connection.DeleteAll<Pop>();
                    connectionRestart.DeleteAll<Pop>();
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var fields = line.Split(';');
                        var songsData = new Pop
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
}
