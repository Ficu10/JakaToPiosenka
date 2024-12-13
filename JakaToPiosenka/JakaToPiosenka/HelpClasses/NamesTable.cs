using JakaToPiosenka.KalamburyClasses;
using JakaToPiosenka.MusicClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace JakaToPiosenka.HelpClasses
{
    public class NamesTable
    {
        public static Dictionary<string, Type> namesTable = new Dictionary<string, Type>
                {
                    { "AllSongs", typeof(AllSongs) },
                    { "FairyTales", typeof(FairyTales) },
                    { "Pop", typeof(Pop) },
                    { "Rock", typeof(Rock) },
                    { "UsersMusic", typeof(UsersMusic) },
                    { "Rap", typeof(Rap) },
                    { "The80", typeof(The80) },
                    { "The80English", typeof(The80English) },
                    { "The80Polish", typeof(The80Polish) },
                    { "RapEnglish", typeof(RapEnglish) },
                    { "RapPolish", typeof(RapPolish) },
                    { "PopEnglish", typeof(PopEnglish) },
                    { "PopPolish", typeof(PopPolish) },
                    { "RockEnglish", typeof(RockEnglish) },
                    { "RockPolish", typeof(RockPolish) },
                    { "Youtube", typeof(Youtube) },
                    { "Children", typeof(Children) },
                    { "Countries", typeof(Countries) },
                    { "Emotions", typeof(Emotions) },
                    { "FictionalCharacter", typeof(FictionalCharacter) },
                    { "HistoricalCharacter", typeof(HistoricalCharacter) },
                    { "Jobs", typeof(Jobs) },
                    { "Movies", typeof(Movies) },
                    { "Series", typeof(Series) },
                    { "Tales", typeof(Tales) },
                    { "Words", typeof(Words) },
                    { "Carols", typeof(Carols) },
                    { "ChristmasSongs", typeof(ChristmasSongs) },
                    { "Animals", typeof(Animals) },
                    { "AdultMixed", typeof(AdultMixed) },
                    { "Celebrities", typeof(Celebrities) },
                    { "DailyLife", typeof(DailyLife) },
                    { "Poland", typeof(Poland) },
                    { "Rhymes", typeof(Rhymes) },
                    { "ScienceTopics", typeof(ScienceTopics) },
                    { "Sports", typeof(Sports) }
                };
    }
}
