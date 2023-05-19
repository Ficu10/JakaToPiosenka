using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JakaToPiosenka
{
    abstract class MusicTypes
    {
        public virtual string Title { get; set; }
        public virtual string Author { get; set; }
        public virtual void Load() { }

        public virtual void Delete() { }

        public virtual void StartGame() { }
    }
}
