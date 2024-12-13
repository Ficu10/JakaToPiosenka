using SQLite;
using System;
using System.IO;

namespace JakaToPiosenka.HelpClasses
{
    public static class MuteHelper
    {
        private static readonly SQLiteConnection connection;

        // Static constructor to initialize the database
        static MuteHelper()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Settings.db3");
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<MuteSetting>();
        }

        // Get the mute state (default to false if not set)
        public static bool GetMuteState()
        {
            var setting = connection.Find<MuteSetting>("Mute");
            return setting?.IsMuted ?? false; // Default to false if no record exists
        }

        // Set the mute state
        public static void SetMuteState(bool isMuted)
        {
            var existingSetting = connection.Find<MuteSetting>("Mute");
            if (existingSetting != null)
            {
                existingSetting.IsMuted = isMuted;
                connection.Update(existingSetting);
            }
            else
            {
                connection.Insert(new MuteSetting { Key = "Mute", IsMuted = isMuted });
            }
        }
    }

    // Class to represent the mute setting
    public class MuteSetting
    {
        [PrimaryKey]
        public string Key { get; set; }
        public bool IsMuted { get; set; }
    }
}
