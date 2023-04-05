using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JakaToPiosenka
{
    public class SongsAndAuthors
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }
    }
}
