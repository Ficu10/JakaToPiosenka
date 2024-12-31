using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace JakaToPiosenka.HelpClasses
{
    internal class LinkToItemHelper
    {
        public enum LinkType
        {
            SONGS,
            KALAMBURY
        }

        public static string GetLinkToItem(
            string itemTitle, AllData el,
            LinkType linkType
        )
        {
            if (!itemTitle.Equals(el.Title))
            {
                throw new Exception("Titles should be the same - something's wrong here!");
            }
            string linkToItemBase;
            string searchQueryComponent = itemTitle.Replace(' ', '+');
            if (linkType.Equals(LinkType.SONGS))
            {
                linkToItemBase = "https://www.youtube.com/results?search_query=";
                searchQueryComponent += '+';
                searchQueryComponent += el.Prompt.Replace(' ', '+');
            }
            else if (linkType.Equals(LinkType.KALAMBURY))
            {
                linkToItemBase = "https://www.google.pl/search?q=";
            }
            else
            {
                throw new NotImplementedException(linkType.ToString());
            }
            return linkToItemBase + searchQueryComponent;
        }

        internal static async Task OpenURL(object sender)
        {
            string linkToItem = ((ImageButton)sender).BindingContext as string;
            await Launcher.OpenAsync(linkToItem);
        }
    }
}
