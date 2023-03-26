using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuessTheSong
{
    public class NamesOfSongs
    {
        [PrimaryKey, AutoIncrement]

        public string Title { get; set; }

        public string Author { get; set; }

    }
}