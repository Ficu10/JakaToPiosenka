using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class UsersMusic : MusicTypes
    {
        public static List<AllSongs> usersMusicSongsList;
        public static List<AllSongs> usersMusicSongsListRestart;

        public static SQLiteConnection dbUsersMusic;
        public static SQLiteConnection dbUsersMusicRestart;


        public override void Load()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            
            dbUsersMusic = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AllSongsDatabase.db3"));
            dbUsersMusicRestart = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AllSongsDatabaseRestart.db3"));
            dbUsersMusic.CreateTable<AllSongs>();
            dbUsersMusicRestart.CreateTable<AllSongs>();
            usersMusicSongsList = dbUsersMusic.Table<AllSongs>().ToList();
            usersMusicSongsListRestart = usersMusicSongsList;
           
        }
    }
}
