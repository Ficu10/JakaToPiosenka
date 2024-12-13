using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JakaToPiosenka.HelpClasses
{
    public static class MuteHelper
    {
        private static readonly SQLiteConnection db;

        static MuteHelper()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Settings.db3");
            db = new SQLiteConnection(dbPath);
            db.CreateTable<AppSettings>();
           
        }

        public static bool GetMuteState()
        {
            var setting = db.Table<AppSettings>().FirstOrDefault();
            return setting?.IsMuted ?? false; // Default to false if no record exists
        }

        public static void SaveMuteState(bool isMuted)
        {
            var setting = db.Table<AppSettings>().FirstOrDefault();
            if (setting == null)
            {
                db.Insert(new AppSettings { IsMuted = isMuted });
            }
            else
            {
                setting.IsMuted = isMuted;
                db.Update(setting);
            }
        }
    }
    public class AppSettings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public bool IsMuted { get; set; }
    }

}
