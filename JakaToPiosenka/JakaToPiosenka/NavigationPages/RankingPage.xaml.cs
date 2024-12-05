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

            // Dodaj graczy do listy z przypisanymi miejscami
            int place = 1;
            foreach (var player in playersFromDb)
            {
                PlayersRanked.Add(new RankedPlayer
                {
                    Place = place++,
                    Name = player.Name,
                    GamesNumber = player.GamesNumber,
                    Points = player.Points
                });
            }

            // Ustaw BindingContext dla strony
            BindingContext = this;
        }
    }
}
