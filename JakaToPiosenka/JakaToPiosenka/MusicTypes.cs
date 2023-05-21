using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JakaToPiosenka
{
    abstract class MusicTypes
    {
        public static SQLiteConnection connection = new SQLiteConnection(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Music.db3"));
        public static SQLiteConnection connectionRestart = new SQLiteConnection(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MusicRestart.db3"));
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual void Load() { }

        public virtual void Delete() { }

        public virtual void StartGame() { }
    }
}
