using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JakaToPiosenka.MusicClasses
{
    abstract class MusicTypes
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        public static SQLiteConnection connection = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Music.db3"));
        public static SQLiteConnection connectionRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MusicRestart.db3"));
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual void Load() { }

        public async virtual void Import(string text) { }

        public virtual void Delete() { }

        public virtual void StartGame() { }
    }
}
