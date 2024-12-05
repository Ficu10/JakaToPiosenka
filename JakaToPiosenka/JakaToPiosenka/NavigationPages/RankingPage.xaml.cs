using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace JakaToPiosenka
{
    public partial class RankingPage : ContentPage
    {
        // Klasa pomocnicza dla graczy z miejscami w rankingu
        public class RankedPlayer
        {
            public int Place { get; set; } // Miejsce w rankingu
            public string Name { get; set; }
            public int Points { get; set; }
            public int GamesNumber { get; set; }
            public Color BackgroundColor { get; set; } // Kolor wiersza
        }

        // Lista graczy z miejscami
        public ObservableCollection<RankedPlayer> PlayersRanked { get; set; } = new ObservableCollection<RankedPlayer>();

        public RankingPage()
        {
            InitializeComponent();

            // Pobierz graczy z bazy danych
            var playersFromDb = Multiplayer.GetAllPlayers()
                                           .OrderByDescending(p => p.Points) // Sortuj po punktach malejąco
                                           .ToList();

            // Dodaj graczy do listy z przypisanymi miejscami i kolorami
            int place = 1;
            foreach (var player in playersFromDb)
            {
                Color rowColor;

                // Przypisz kolor na podstawie miejsca
                if (place == 1)
                {
                    rowColor = Color.Gold; // Złoty dla 1 miejsca
                }
                else if (place == 2)
                {
                    rowColor = Color.Silver; // Srebrny dla 2 miejsca
                }
                else if (place == 3)
                {
                    rowColor = Color.FromHex("#cd7f32"); // Brązowy dla 3 miejsca
                }
                else
                {
                    rowColor = Color.FromHex("#696968"); // Niebieski dla reszty
                }

                // Dodaj gracza do listy
                PlayersRanked.Add(new RankedPlayer
                {
                    Place = place++,
                    Name = player.Name,
                    GamesNumber = player.GamesNumber,
                    Points = player.Points,
                    BackgroundColor = rowColor // Ustaw kolor tła
                });
            }

            // Ustaw BindingContext dla strony
            BindingContext = this;
        }
    }
}
