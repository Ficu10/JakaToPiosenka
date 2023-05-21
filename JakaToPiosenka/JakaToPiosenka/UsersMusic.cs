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
        public override void Load()
        {
            connection.CreateTable<UsersMusic>();
            connectionRestart.CreateTable<UsersMusic>();

        }
    }
}
