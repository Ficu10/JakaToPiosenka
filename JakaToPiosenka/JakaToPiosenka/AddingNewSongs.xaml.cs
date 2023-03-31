﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddingNewSongs : ContentPage
	{
		public AddingNewSongs ()
		{
			InitializeComponent ();
            switch (MainPage.gameMode)
            {
                case "allSongs":
                    SongsCollection.ItemsSource = Game.songsTab;
                    break;
                case "Disney":
                    SongsCollection.ItemsSource = Game.songsTabFairyTales;
                    break;
                case "Pop":
                    SongsCollection.ItemsSource = Game.songsTabPop;
                    break;
                case "Rock":
                    SongsCollection.ItemsSource = Game.songsTabRock;
                    break;
                case "UsersMusic":
                    SongsCollection.ItemsSource = Game.songsTabUsersMusic;
                    break;
                case "Rap":
                    SongsCollection.ItemsSource = Game.songsTabRap;
                    break;

            }
        }

        private void addSong_Clicked(object sender, EventArgs e)
        {

        }
    }
}