using JakaToPiosenka.HelpClasses;
using SQLite;
using System.IO;

namespace JakaToPiosenka
{
    internal class UsersMusic : AllPasswords
    {
        // Właściwość FileName zwraca pustą wartość, ponieważ UsersMusic nie korzysta z pliku.
        public override string FileName => "UserMusic";

       
    }
}
