using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace JakaToPiosenka
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Game : ContentPage
    {

        int songId;
        public static int pointsCounter;
        bool newGame = true;
        int seconds;
        bool endOfQuestion = false;
        bool goodAnswer = false;
        bool answered = false;


        public static List<string> authorsTab = new List<string> { "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Myslovitz", "Myslovitz", "Myslovitz", "Happysad", "Republika", "Republika", "Wilki", "Wilki", "Kombi", "Urszula", "Urszula", "Luxtorpeda", "Maanam", "Maanam", "Czerwone Gitary", "Kobranocka", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Czesław Niemen", "Dżem", "Dżem", "Budka Suflera", "Budka Suflera", "Budka Suflera", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Black Eyed Peas", "Black Eyed Peas", "Black Eyed Peas", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Beyoncé", "Beyoncé", "Justin Timberlake", "Justin Timberlake", "Nelly Furtado", "Nelly Furtado", "Nelly Furtado", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Bob Dylan", "Rammstein", "Rammstein", "Rammstein", "Oasis", "Panic! at the Disco", "Panic! at the Disco", "Panic! at the Disco", "Pink Floyd", "Pink Floyd", "Red Hot Chili Peppers", "Bon Jovi", "The Cranberries", "Metallica", "Metallica", "Metallica", "Guns N' Roses", "Guns N' Roses", "The Doors", "System of a Down", "System of a Down", "System of a Down", "Green Day", "Green Day", "Green Day", "Green Day", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Nirvana", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Enej", "Enej", "Enej", "Enej", "Dawid Podsiadło", "Dawid Podsiadło", "Dawid Podsiadło", "Dawid Podsiadło", "Carly Ray Japsen", "Avicii", "Avicii", "Avicii", "Kazik Staszewski", "Kazik Staszewski", "Kazik Staszewski", "Kazik Staszewski", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "AC/DC", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Shawn Mendes", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira" };
        public static List<string> songsTab = new List<string> { "Bo jesteś Ty", "Chciałem być", "Za Tobą pójdę jak na bal", "Parostatek", "Zatańczysz ze mną jeszcze raz", "Ale jazz!", "Szampan", "No sory", "ten Stan", "Melodia", "2:00", "Długość dźwięku samotności", "Scenariusz dla moich sąsiadów", "Peggy Brown", "Zanim pójdę", "Mamona", "Telefony", "Nie Stało Się Nic", "Baśka", "Słodkiego miłego życia", "Koń na Biegunach", "Dmuchawce, latawce", "Autystyczny", "Cykady na Cykladach", "Kocham Cię, kochanie moje", "Ciągle pada", "Kocham Cię jak Irlandię", "Zawsze Tam Gdzie Ty", "Kryzysowa Narzeczona", "Tańcz głupia, tańcz", "Mniej niż Zero", "Stacja Warszawa", "Dziwny jest ten świat", "Wehikuł czasu", "Whisky", "Jolka, Jolka pamiętasz", "Takie Tango", "Bal wszystkich świętych", "Nie płacz Ewka", "Wszystko ma swój czas ", "Chcemy być sobą", "Autobiografia ", "Kołysanka dla nieznajomej", "Ale w koło jest wesoło", "GIRL LIKE ME", "Pump It", "I Gotta Feeling", "Payphone ", "Animals", "Sugar", "One More Night", "Moves Like Jagger", "Single Ladies", "Halo", "Mirrors", "Can't Stop The Feeling", "Say It Right", "Maneater ", "Promiscuous", "Umbrella", "Only Girl (In the World)", "Disturbia", "Don't Stop the Music", "Diamonds", "Love The Way You Lie", "Roar", "E.T.", "Last Friday Night (T.G.I.F.)", "I Kissed a Girl", "Hot n Cold", "California Gurls", "Dark Horse", "Firework", "Hit Me Baby One More Time", "Oops!...I Did It Again", "I Wanna Go", "Toxic", "Womanizer", "Bad Romance", "Poker Face", "Paparazzi", "Applause", "Alejandro", "Knockin' on heaven's door", "DEUTSCHLAND", "Du Hast", "Sonne", "Wonderwall ", "Girls / Girls / Boys", "High Hopes", "Let's Kill Tonight", "High Hopes", "Another brick in the wall part II", "Californication", "It's My Life", "Zombie", "Nothing Else Matters", "Enter Sandman", "Master of Puppets", "Sweet Child O' Mine", "Knockin' On Heaven's Door", "Riders On The Storm", "Chop Suey", "Aerials", "Lonely Day", "Boulevard of Broken Dreams", "Holiday", "Wake Me Up When September Ends", "American Idiot", "Numb", "What I've Done", "Castle of Glas", "In The End", "Somewhere I Belong", "Smells Like Teen Spirit", "Slipping Through My Fingers", "Lay All Your Love On Me", "Waterloo", "Mamma Mia", "Dancing Queen", "Gimmie! Gimmie! Gimmie!", "Money, Money, Money", "Billie Jean", "Beat It", "Smooth Criminal", "Thriller", "Black Or White", "They Don't Care About Us", "Good Old-Fashioned Lover Boy", "Killer Queen", "Love of My Life", "The Show Must Go On", "Radio Ga Ga", "I Want to Break Free", "Don't Stop Me Now", "Bohemian Rhapsody", "We Will Rock You", "Please Mr. Postman", "Twist and Shout", "I Want To Hold Your Hand", "Let It Be", "Hey Jude", "Yesterday", "Here Comes the Sun", "Skrzydlate ręce", "Tak smakuje życie", "Radio Hello", "Lili", "Małomiasteczkowy", "Nie Ma Fal", "Trójkąty i Kwadraty", "W dobrą stronę", "Call me mayby", "You Make Me", "Hey Brother", "Wake me up", "Arahja", "Baranek", "Gdy nie ma dzieci", "12 Groszy", "Fallen Leaves", "Red Flag", "Surrender", "Rusted from the Rain", "Devil in a Midnight Mass", "Devil on My Shoulder", "Highway To Hell", "Galway Girl", "I See Fire", "Bad Habits", "Shivers", "Perfect", "Shape Of You", "Stiches", "Idzie Zima", "Niemożliwe", "Dziś późno pójdę spać", "Wzięli zamknęli mi klub", "Loca", "Waka Waka", "Hips Don't Lie", "She Wolf", "Whenever, Wherever", "Rabiosa", "Can't remember to forget you" };
                      
        public static List<string> authorsTabFairyTales = new List<string> { "Kurczak Mały", "Smerfy", "Reksio", "Scooby Doo", "Nowe Przygody Kubusia Puchatka", "Gumisie", "Kacze Opowieści", "Chip i Dale", "Coco", "Coco", "Coco", "Coco", "Madagaskar", "Pingwiny z Madagaskaru", "Toy Story", "Planeta Skarbów", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Herkules", "Herkules", "Herkules", "Herkules", "Herkules", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Mulan", "Mulan", "Mulan", "Mulan", "Mulan", "Mulan", "Tarzan", "Tarzan", "Tarzan", "Tarzan", "Piękna i Bestia", "Piękna i Bestia", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Aladdyn", "Aladdyn", "Aladdyn", "Aladdyn", "Zaplątani", "Zaplątani", "Zaplątani", "Zaplątani", "Księżniczka Łabędzi", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Rogate Ranczo", "Rogate Ranczo", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu 2", "Kraina Lodu 2", "Kraina Lodu 2", "Merida Waleczna", "Barbie Księżniczka i Żebraczka", "Barbie Księżniczka i Żebraczka", "Barbie Księżniczka i Żebraczka", "Lilo i Stitch", "Lilo i Stitch", "Lilo i Stitch", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Vaiana", "Vaiana", "Vaiana", "Vaiana", "Pocahontas", "Pocahontas", "Pocahontas" };
        public static List<string> songsTabFairyTales = new List<string> { "Ten mały Błąd", "Intro", "Intro", "Intro", "Intro", "Intro", "Intro", "Intro", "La Llorona", "Corazón", "Pamiętaj Mnie", "Un Poco Loco", "Wyginam śmiało ciało", "Ja i mój JJ", "Ty Druha we mnie masz", "Jestem Kimś", "Chcę być Tam", "Jaskinia Cudów", "Niezwykły Gość", "Na Odwyrtkę", "Z Dna Piekieł", "Modlitwa Esmeraldy", "Dźwięk Dzwonów Notre Dame", "Aleja Gwiazd", "Zaprawdę Wierzcie Mi", "Droga Mi Nie Straszna", "Straciłem Nadzieję", "Zero To Hero", "Upendi", "Miłość Drogę Zna", "Jeden Głos", "Duch Żyje w Nas", "Hakuna Matata", "Miłość Rośnie wokół Nas", "Strasznie już być Tym Królem chcę", "Krąg życia", "Taka jak inne pragnę być", "O tym lekcja ta", "Za którą walczyć chcesz", "Zrobię Mężczyzn z Was", "Lustro", "Zaszczyt Nam przyniesie To", "W Mym Sercu Twój Dom", "Obcy tacy jak Ja", "Człowieka Syn", "Światy Dwa", "Gaston", "Gościem Bądź", "Chciałabym być tam, gdzie ludzie są", "Całuj Ją", "Na Morza Dnie", "Przez te chwilę czuję, że", "Morze i stały Ląd", "Tańcz Seniora", "O Krok", "Nie ma takich dwóch", "Książe Ali", "Wspaniały Świat", "Cierpliwie Czekam", "Marzenie Mam", "Po raz pierwszy widzę Blask", "Nowe Dni", "Na dłużej niż na zawsze", "Transformacja", "Bratu Równy Brat", "Już wyruszać Czas", "I Ty możesz zostać z Nami", "Już Nikt już Nic", "To własnie Dom", "Na południu w Luizianie", "Przyjaciele z Zaświatów", "Prawie udało się", "Poszukaj Głębiej", "W Drodze Przez Bajoro", "Skrawek Nieba Mam", "Yodle-Adle-Eedle-Idle-Oo", "To co ukochałem", "Uwolnij Nas", "Przez Nieba Wzrok", "Nie z Patałachami grasz", "Plagi", "Cuda dzieją się", "Ulepimy Dziś Bałwana", "Miłość stanęła w drzwiach", "Pierwszy raz jak sięga Pamięć", "Lód w Lecie", "Mam Tę Moc", "Nie Ten Tego", "Pokaż się", "Chcę uwierzyć Snom", "Którą wybrać Mam z Dróg", "Chwytam Wiatr", "Chcę naprawdę wolną być", "Ja tak jak Ty", "Jesteś koto-psem", "He Mele No Lilo", "Hawaiian Roller Coaster", "Burning Love", "Długo i Szczęśliwie", "O Krok", "Skąd wiedzieć ma", "Pracować będzie lżej", "O miłosnym Pocałunku śnię", "Będę wracał", "Złaź ze Mnie no już", "Chcę Wolnym Być", "Oto Ja", "Pół Kroku Stąd", "Na Drodze Tej", "Drobnostka", "Błyszczeć", "Kolorowy Wiatr", "Ten za Łukiem Rzeki Świat", "Dzicy Są!" };
                      
        public static List<string> authorsTabRock = new List<string> { "Golden Life", "Eric Clapton", "Myslovitz", "Myslovitz", "Myslovitz", "Happysad", "Republika", "Republika", "Wilki", "Wilki", "Kombii", "Urszula", "Urszula", "Luxtorpeda", "Maanam", "Maanam", "Czerwone Gitary", "Kobranocka", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Czesław Niemen", "Dżem", "Dżem", "Budka Suflera", "Budka Suflera", "Budka Suflera", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Bob Dylan", "Rammstein", "Rammstein", "Rammstein", "Oasis", "Panic! at the Disco", "Panic! at the Disco", "Panic! at the Disco", "Pink Floyd", "Pink Floyd", "Red Hot Chili Peppers", "Bon Jovi", "The Cranberries", "Metallica", "Metallica", "Metallica", "Guns N' Roses", "Guns N' Roses", "The Doors", "System of a Down", "System of a Down", "System of a Down", "Green Day", "Green Day", "Green Day", "Green Day", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Nirvana", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "AC/DC" };
        public static List<string> songsTabRock = new List<string> { "Oprócz(błękitnego nieba)", "Tears in Heaven", "Długość dźwięku samotności", "Scenariusz dla moich sąsiadów", "Peggy Brown", "Zanim pójdę", "Mamona", "Telefony", "Nie Stało Się Nic", "Baśka", "Słodkiego miłego życia", "Koń na Biegunach", "Dmuchawce, latawce", "Autystyczny", "Cykady na Cykladach", "Kocham Cię, kochanie moje", "Ciągle pada", "Kocham Cię jak Irlandię", "Zawsze Tam Gdzie Ty", "Kryzysowa Narzeczona", "Tańcz głupia, tańcz", "Mniej niż Zero", "Stacja Warszawa", "Dziwny jest ten świat", "Wehikuł czasu", "Whisky", "Jolka, Jolka pamiętasz", "Takie Tango", "Bal wszystkich świętych", "Nie płacz Ewka", "Wszystko ma swój czas ", "Chcemy być sobą", "Autobiografia ", "Kołysanka dla nieznajomej", "Ale w koło jest wesoło", "Knockin' on heaven's door", "DEUTSCHLAND", "Du Hast", "Sonne", "Wonderwall ", "Girls / Girls / Boys", "High Hopes", "Let's Kill Tonight", "High Hopes", "Another brick in the wall", "Californication", "It's My Life", "Zombie", "Nothing Else Matters", "Enter Sandman", "Master of Puppets", "Sweet Child O' Mine", "Knockin' On Heaven's Door", "Riders On The Storm", "Chop Suey", "Aerials", "Lonely Day", "Boulevard of Broken Dreams", "Holiday", "Wake Me Up When September Ends", "American Idiot", "Numb", "What I've Done", "Castle of Glas", "In The End", "Somewhere I Belong", "Smells Like Teen Spirit", "Good Old-Fashioned Lover Boy", "Killer Queen", "Love of My Life", "The Show Must Go On", "Radio Ga Ga", "I Want to Break Free", "Don't Stop Me Now", "Bohemian Rhapsody", "We Will Rock You", "Please Mr. Postman", "Yesterday", "Twist and Shout", "I Want To Hold Your Hand", "Let It Be", "Hey Jude", "Here Comes the Sun", "Fallen Leaves", "Red Flag", "Surrender", "Rusted from the Rain", "Devil in a Midnight Mass", "Devil on My Shoulder", "Highway To Hell" };
                      
        public static List<string> authorsTabPop = new List<string> { " CHARLES & EDDIE", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "Dr.Alban", "Mr.Big", " CHARLES & EDDIE", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Black Eyed Peas", "Black Eyed Peas", "Black Eyed Peas", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Beyoncé", "Beyoncé", "Justin Timberlake", "Justin Timberlake", "Nelly Furtado", "Nelly Furtado", "Nelly Furtado", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Shawn Mendes", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira" };
        public static List<string> songsTabPop = new List<string> { "Would I Lie To You?", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "It's my life", "To be with You", "Would I Lie To You?", "Bo jesteś Ty", "Chciałem być", "Za Tobą pójdę jak na bal", "Parostatek", "Zatańczysz ze mną jeszcze raz", "Ale jazz!", "Szampan", "No sory", "ten Stan", "Melodia", "2:00", "GIRL LIKE ME", "Pump It", "I Gotta Feeling", "Payphone ", "Animals", "Sugar", "One More Night", "Moves Like Jagger", "Single Ladies", "Halo", "Mirrors", "Can't Stop The Feeling", "Say It Right", "Maneater ", "Promiscuous", "Umbrella", "Only Girl (In the World)", "Disturbia", "Don't Stop the Music", "Diamonds", "Love The Way You Lie", "Roar", "E.T.", "Last Friday Night (T.G.I.F.)", "I Kissed a Girl", "Hot n Cold", "California Gurls", "Dark Horse", "Firework", "Hit Me Baby One More Time", "Oops!...I Did It Again", "I Wanna Go", "Toxic", "Womanizer", "Bad Romance", "Poker Face", "Paparazzi", "Applause", "Alejandro", "Slipping Through My Fingers", "Lay All Your Love On Me", "Waterloo", "Mamma Mia", "Dancing Queen", "Gimmie! Gimmie! Gimmie!", "Money, Money, Money", "Billie Jean", "Beat It", "Smooth Criminal", "Heal The World", "Thriller", "Black Or White", "They Don't Care About Us", "Galway Girl", "I See Fire", "Bad Habits", "Perfect", "Shape Of You", "Stiches", "Idzie Zima", "Niemożliwe", "Dziś późno pójdę spać", "Wzięli zamknęli mi klub", "Loca", "Waka waka", "Rabiosa", "Can't remember to forget you", "Hips Don't Lie", "She Wolf", "Whenever, Wherever" };

        public static List<string> authorsTabRap = new List<string> { "Twenty one pilots", "Twenty one pilots", "White 2115", "White 2115", "White 2115", "Tymek", "Tymek", "Tymek", "Tymek", "Kizo feat. Malik Montana", "Malik Montana", "Malik Montana", "Żabson", "Żabson", "Żabson", "Żabson", "Kizo", "Kizo", "Kizo", "Kizo", "Kizo", "Kizo", "Sobel", "Sobel", "Sobel", "Sobel", "Sobel", "Mata", "Mata", "Mata", "Mata", "Mata", "Mata", "Mata", "Kuban", "Kuban", "Kuban", "Kuban", "Kuban", "PRO8L3M", "PRO8L3M", "PRO8L3M", "PRO8L3M", "PRO8L3M", "TACONAFIDE", "TACONAFIDE", "TACONAFIDE", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Wiz Khalifa", "Wiz Khalifa", "Wiz Khalifa", "Kendrick Lamar", "Kendrick Lamar", "Kendrick Lamar", "Macklemore", "Macklemore", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem" };
        public static List<string> songsTabRap = new List<string> { "Ride", "Stressed Out", "Morgan", "California", "Mogę dziś umierać", "Język ciała", "Anioły i Demony", "Rainman", "Poza kontrolą", "CZEMPION", "1szy Nos", "Naaajak", "Puerto Bounce", "Do Ziomów", "Księżniczki", "Sexoholik", "Miasto 24H", "Toskania", "Fitness", "Lucky Punch", "Disney", "Kizo", "Impreza", "Testarossa", "Bandyta", "Fiołkowe Pole", "Cześć, jak się masz?", "100 dni do matury", "Patointeligencja", "Mata Montana", "Schodki", "Kiss cam", "Papuga", "Patoreakcja", "Suki", "DOBzI LUDZIE", "Ta dama", "W taki dzień", "Chore jazdy", "Stówa", "TEB 200-1", "Flary", "Molly", "Skrable", "Kryptowaluty", "8 kobiet", "Tamagotchi", "Nostalgia", "Fiji", "Następna stacja", "6 zer", "Deszcz na betonie", "We Own It", "We Dem Boyz", "Black and Yellow", "Swimming Pools", "Alright", "HUMBLE", "Can't Hold Us", "Trift Shop", "Shake That", "Like Toy Soldiers", "Beautiful", "Just Lose It", "Not Afraid", "When I'm Gone", "Mockingbird", "Rap God", "The Real Slim Shady", "Without Me" };

        public static List<string> authorsTabRapRestart = new List<string> { "Twenty one pilots", "Twenty one pilots", "White 2115", "White 2115", "White 2115", "Tymek", "Tymek", "Tymek", "Tymek", "Kizo feat. Malik Montana", "Malik Montana", "Malik Montana", "Żabson", "Żabson", "Żabson", "Żabson", "Kizo", "Kizo", "Kizo", "Kizo", "Kizo", "Kizo", "Sobel", "Sobel", "Sobel", "Sobel", "Sobel", "Mata", "Mata", "Mata", "Mata", "Mata", "Mata", "Mata", "Kuban", "Kuban", "Kuban", "Kuban", "Kuban", "PRO8L3M", "PRO8L3M", "PRO8L3M", "PRO8L3M", "PRO8L3M", "TACONAFIDE", "TACONAFIDE", "TACONAFIDE", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Wiz Khalifa", "Wiz Khalifa", "Wiz Khalifa", "Kendrick Lamar", "Kendrick Lamar", "Kendrick Lamar", "Macklemore", "Macklemore", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem" };
        public static List<string> songsTabRapRestart = new List<string> { "Ride", "Stressed Out", "Morgan", "California", "Mogę dziś umierać", "Język ciała", "Anioły i Demony", "Rainman", "Poza kontrolą", "CZEMPION", "1szy Nos", "Naaajak", "Puerto Bounce", "Do Ziomów", "Księżniczki", "Sexoholik", "Miasto 24H", "Toskania", "Fitness", "Lucky Punch", "Disney", "Kizo", "Impreza", "Testarossa", "Bandyta", "Fiołkowe Pole", "Cześć, jak się masz?", "100 dni do matury", "Patointeligencja", "Mata Montana", "Schodki", "Kiss cam", "Papuga", "Patoreakcja", "Suki", "DOBzI LUDZIE", "Ta dama", "W taki dzień", "Chore jazdy", "Stówa", "TEB 200-1", "Flary", "Molly", "Skrable", "Kryptowaluty", "8 kobiet", "Tamagotchi", "Nostalgia", "Fiji", "Następna stacja", "6 zer", "Deszcz na betonie", "We Own It", "We Dem Boyz", "Black and Yellow", "Swimming Pools", "Alright", "HUMBLE", "Can't Hold Us", "Trift Shop", "Shake That", "Like Toy Soldiers", "Beautiful", "Just Lose It", "Not Afraid", "When I'm Gone", "Mockingbird", "Rap God", "The Real Slim Shady", "Without Me" };

        public static List<string> authorsTabUsersMusic = new List<string>();
        public static List<string> songsTabUsersMusic = new List<string>();

        public static List<string> authorsTabUsersMusicRestart = new List<string>();
        public static List<string> songsTabUsersMusicRestart = new List<string>();



        public static List<string> authorsTabRestart = new List<string> { "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Myslovitz", "Myslovitz", "Myslovitz", "Happysad", "Republika", "Republika", "Wilki", "Wilki", "Kombi", "Urszula", "Urszula", "Luxtorpeda", "Maanam", "Maanam", "Czerwone Gitary", "Kobranocka", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Czesław Niemen", "Dżem", "Dżem", "Budka Suflera", "Budka Suflera", "Budka Suflera", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Black Eyed Peas", "Black Eyed Peas", "Black Eyed Peas", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Beyoncé", "Beyoncé", "Justin Timberlake", "Justin Timberlake", "Nelly Furtado", "Nelly Furtado", "Nelly Furtado", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Bob Dylan", "Rammstein", "Rammstein", "Rammstein", "Oasis", "Panic! at the Disco", "Panic! at the Disco", "Panic! at the Disco", "Pink Floyd", "Pink Floyd", "Red Hot Chili Peppers", "Bon Jovi", "The Cranberries", "Metallica", "Metallica", "Metallica", "Guns N' Roses", "Guns N' Roses", "The Doors", "System of a Down", "System of a Down", "System of a Down", "Green Day", "Green Day", "Green Day", "Green Day", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Nirvana", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Enej", "Enej", "Enej", "Enej", "Dawid Podsiadło", "Dawid Podsiadło", "Dawid Podsiadło", "Dawid Podsiadło", "Carly Ray Japsen", "Avicii", "Avicii", "Avicii", "Kazik Staszewski", "Kazik Staszewski", "Kazik Staszewski", "Kazik Staszewski", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "AC/DC", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Shawn Mendes", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira" };
        public static List<string> songsTabRestart = new List<string> { "Bo jesteś Ty", "Chciałem być", "Za Tobą pójdę jak na bal", "Parostatek", "Zatańczysz ze mną jeszcze raz", "Ale jazz!", "Szampan", "No sory", "ten Stan", "Melodia", "2:00", "Długość dźwięku samotności", "Scenariusz dla moich sąsiadów", "Peggy Brown", "Zanim pójdę", "Mamona", "Telefony", "Nie Stało Się Nic", "Baśka", "Słodkiego miłego życia", "Koń na Biegunach", "Dmuchawce, latawce", "Autystyczny", "Cykady na Cykladach", "Kocham Cię, kochanie moje", "Ciągle pada", "Kocham Cię jak Irlandię", "Zawsze Tam Gdzie Ty", "Kryzysowa Narzeczona", "Tańcz głupia, tańcz", "Mniej niż Zero", "Stacja Warszawa", "Dziwny jest ten świat", "Wehikuł czasu", "Whisky", "Jolka, Jolka pamiętasz", "Takie Tango", "Bal wszystkich świętych", "Nie płacz Ewka", "Wszystko ma swój czas ", "Chcemy być sobą", "Autobiografia ", "Kołysanka dla nieznajomej", "Ale w koło jest wesoło", "GIRL LIKE ME", "Pump It", "I Gotta Feeling", "Payphone ", "Animals", "Sugar", "One More Night", "Moves Like Jagger", "Single Ladies", "Halo", "Mirrors", "Can't Stop The Feeling", "Say It Right", "Maneater ", "Promiscuous", "Umbrella", "Only Girl (In the World)", "Disturbia", "Don't Stop the Music", "Diamonds", "Love The Way You Lie", "Roar", "E.T.", "Last Friday Night (T.G.I.F.)", "I Kissed a Girl", "Hot n Cold", "California Gurls", "Dark Horse", "Firework", "Hit Me Baby One More Time", "Oops!...I Did It Again", "I Wanna Go", "Toxic", "Womanizer", "Bad Romance", "Poker Face", "Paparazzi", "Applause", "Alejandro", "Knockin' on heaven's door", "DEUTSCHLAND", "Du Hast", "Sonne", "Wonderwall ", "Girls / Girls / Boys", "High Hopes", "Let's Kill Tonight", "High Hopes", "Another brick in the wall part II", "Californication", "It's My Life", "Zombie", "Nothing Else Matters", "Enter Sandman", "Master of Puppets", "Sweet Child O' Mine", "Knockin' On Heaven's Door", "Riders On The Storm", "Chop Suey", "Aerials", "Lonely Day", "Boulevard of Broken Dreams", "Holiday", "Wake Me Up When September Ends", "American Idiot", "Numb", "What I've Done", "Castle of Glas", "In The End", "Somewhere I Belong", "Smells Like Teen Spirit", "Slipping Through My Fingers", "Lay All Your Love On Me", "Waterloo", "Mamma Mia", "Dancing Queen", "Gimmie! Gimmie! Gimmie!", "Money, Money, Money", "Billie Jean", "Beat It", "Smooth Criminal", "Thriller", "Black Or White", "They Don't Care About Us", "Good Old-Fashioned Lover Boy", "Killer Queen", "Love of My Life", "The Show Must Go On", "Radio Ga Ga", "I Want to Break Free", "Don't Stop Me Now", "Bohemian Rhapsody", "We Will Rock You", "Please Mr. Postman", "Twist and Shout", "I Want To Hold Your Hand", "Let It Be", "Hey Jude", "Yesterday", "Here Comes the Sun", "Skrzydlate ręce", "Tak smakuje życie", "Radio Hello", "Lili", "Małomiasteczkowy", "Nie Ma Fal", "Trójkąty i Kwadraty", "W dobrą stronę", "Call me mayby", "You Make Me", "Hey Brother", "Wake me up", "Arahja", "Baranek", "Gdy nie ma dzieci", "12 Groszy", "Fallen Leaves", "Red Flag", "Surrender", "Rusted from the Rain", "Devil in a Midnight Mass", "Devil on My Shoulder", "Highway To Hell", "Galway Girl", "I See Fire", "Bad Habits", "Shivers", "Perfect", "Shape Of You", "Stiches", "Idzie Zima", "Niemożliwe", "Dziś późno pójdę spać", "Wzięli zamknęli mi klub", "Loca", "Waka Waka", "Hips Don't Lie", "She Wolf", "Whenever, Wherever", "Rabiosa", "Can't remember to forget you" };

        public static List<string> authorsTabFairyTalesRestart = new List<string> { "Kurczak Mały", "Smerfy", "Reksio", "Scooby Doo", "Nowe Przygody Kubusia Puchatka", "Gumisie", "Kacze Opowieści", "Chip i Dale", "Coco", "Coco", "Coco", "Coco", "Madagaskar", "Pingwiny z Madagaskaru", "Toy Story", "Planeta Skarbów", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Herkules", "Herkules", "Herkules", "Herkules", "Herkules", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Mulan", "Mulan", "Mulan", "Mulan", "Mulan", "Mulan", "Tarzan", "Tarzan", "Tarzan", "Tarzan", "Piękna i Bestia", "Piękna i Bestia", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Aladdyn", "Aladdyn", "Aladdyn", "Aladdyn", "Zaplątani", "Zaplątani", "Zaplątani", "Zaplątani", "Księżniczka Łabędzi", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Rogate Ranczo", "Rogate Ranczo", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu 2", "Kraina Lodu 2", "Kraina Lodu 2", "Merida Waleczna", "Barbie Księżniczka i Żebraczka", "Barbie Księżniczka i Żebraczka", "Barbie Księżniczka i Żebraczka", "Lilo i Stitch", "Lilo i Stitch", "Lilo i Stitch", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Vaiana", "Vaiana", "Vaiana", "Vaiana", "Pocahontas", "Pocahontas", "Pocahontas" };
        public static List<string> songsTabFairyTalesRestart = new List<string> { "Ten mały Błąd", "Intro", "Intro", "Intro", "Intro", "Intro", "Intro", "Intro", "La Llorona", "Corazón", "Pamiętaj Mnie", "Un Poco Loco", "Wyginam śmiało ciało", "Ja i mój JJ", "Ty Druha we mnie masz", "Jestem Kimś", "Chcę być Tam", "Jaskinia Cudów", "Niezwykły Gość", "Na Odwyrtkę", "Z Dna Piekieł", "Modlitwa Esmeraldy", "Dźwięk Dzwonów Notre Dame", "Aleja Gwiazd", "Zaprawdę Wierzcie Mi", "Droga Mi Nie Straszna", "Straciłem Nadzieję", "Zero To Hero", "Upendi", "Miłość Drogę Zna", "Jeden Głos", "Duch Żyje w Nas", "Hakuna Matata", "Miłość Rośnie wokół Nas", "Strasznie już być Tym Królem chcę", "Krąg życia", "Taka jak inne pragnę być", "O tym lekcja ta", "Za którą walczyć chcesz", "Zrobię Mężczyzn z Was", "Lustro", "Zaszczyt Nam przyniesie To", "W Mym Sercu Twój Dom", "Obcy tacy jak Ja", "Człowieka Syn", "Światy Dwa", "Gaston", "Gościem Bądź", "Chciałabym być tam, gdzie ludzie są", "Całuj Ją", "Na Morza Dnie", "Przez te chwilę czuję, że", "Morze i stały Ląd", "Tańcz Seniora", "O Krok", "Nie ma takich dwóch", "Książe Ali", "Wspaniały Świat", "Cierpliwie Czekam", "Marzenie Mam", "Po raz pierwszy widzę Blask", "Nowe Dni", "Na dłużej niż na zawsze", "Transformacja", "Bratu Równy Brat", "Już wyruszać Czas", "I Ty możesz zostać z Nami", "Już Nikt już Nic", "To własnie Dom", "Na południu w Luizianie", "Przyjaciele z Zaświatów", "Prawie udało się", "Poszukaj Głębiej", "W Drodze Przez Bajoro", "Skrawek Nieba Mam", "Yodle-Adle-Eedle-Idle-Oo", "To co ukochałem", "Uwolnij Nas", "Przez Nieba Wzrok", "Nie z Patałachami grasz", "Plagi", "Cuda dzieją się", "Ulepimy Dziś Bałwana", "Miłość stanęła w drzwiach", "Pierwszy raz jak sięga Pamięć", "Lód w Lecie", "Mam Tę Moc", "Nie Ten Tego", "Pokaż się", "Chcę uwierzyć Snom", "Którą wybrać Mam z Dróg", "Chwytam Wiatr", "Chcę naprawdę wolną być", "Ja tak jak Ty", "Jesteś koto-psem", "He Mele No Lilo", "Hawaiian Roller Coaster", "Burning Love", "Długo i Szczęśliwie", "O Krok", "Skąd wiedzieć ma", "Pracować będzie lżej", "O miłosnym Pocałunku śnię", "Będę wracal", "Złaź ze Mnie no już", "Chcę Wolnym Być", "Oto Ja", "Pół Kroku Stąd", "Na Drodze Tej", "Drobnostka", "Błyszczeć", "Kolorowy Wiatr", "Ten za Łukiem Rzeki Świat", "Dzicy Są!" };

        public static List<string> authorsTabRockRestart = new List<string> { "Myslovitz", "Myslovitz", "Myslovitz", "Happysad", "Republika", "Republika", "Wilki", "Wilki", "Kombii", "Kombii", "Urszula", "Urszula", "Luxtorpeda", "Maanam", "Maanam", "Czerwone Gitary", "Kobranocka", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Czesław Niemen", "Dżem", "Dżem", "Budka Suflera", "Budka Suflera", "Budka Suflera", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Bob Dylan", "Rammstein", "Rammstein", "Rammstein", "Oasis", "Panic! at the Disco", "Panic! at the Disco", "Panic! at the Disco", "Pink Floyd", "Pink Floyd", "Red Hot Chili Peppers", "Bon Jovi", "The Cranberries", "Metallica", "Metallica", "Metallica", "Guns N' Roses", "Guns N' Roses", "The Doors", "System of a Down", "System of a Down", "System of a Down", "Green Day", "Green Day", "Green Day", "Green Day", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Nirvana", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "AC/DC" };
        public static List<string> songsTabRockRestart = new List<string> { "Długość dźwięku samotności", "Scenariusz dla moich sąsiadów", "Peggy Brown", "Zanim pójdę", "Mamona", "Telefony", "Nie Stało Się Nic", "Baśka", "Słodkiego miłego życia", " Pokolenie", "Koń na Biegunach", "Dmuchawce, latawce", "Autystyczny", "Cykady na Cykladach", "Kocham Cię, kochanie moje", "Ciągle pada", "Kocham Cię jak Irlandię", "Zawsze Tam Gdzie Ty", "Kryzysowa Narzeczona", "Tańcz głupia, tańcz", "Mniej niż Zero", "Stacja Warszawa", "Dziwny jest ten świat", "Wehikuł czasu", "Whisky", "Jolka, Jolka pamiętasz", "Takie Tango", "Bal wszystkich świętych", "Nie płacz Ewka", "Wszystko ma swój czas ", "Chcemy być sobą", "Autobiografia ", "Kołysanka dla nieznajomej", "Ale w koło jest wesoło", "Knockin' on heaven's door", "DEUTSCHLAND", "Du Hast", "Sonne", "Wonderwall ", "Girls / Girls / Boys", "High Hopes", "Let's Kill Tonight", "High Hopes", "Another brick in the wall", "Californication", "It's My Life", "Zombie", "Nothing Else Matters", "Enter Sandman", "Master of Puppets", "Sweet Child O' Mine", "Knockin' On Heaven's Door", "Riders On The Storm", "Chop Suey", "Aerials", "Lonely Day", "Boulevard of Broken Dreams", "Holiday", "Wake Me Up When September Ends", "American Idiot", "Numb", "What I've Done", "Castle of Glas", "In The End", "Somewhere I Belong", "Smells Like Teen Spirit", "Good Old-Fashioned Lover Boy", "Killer Queen", "Love of My Life", "The Show Must Go On", "Radio Ga Ga", "I Want to Break Free", "Don't Stop Me Now", "Bohemian Rhapsody", "We Will Rock You", "Please Mr. Postman", "Yesterday", "Twist and Shout", "I Want To Hold Your Hand", "Let It Be", "Hey Jude", "Here Comes the Sun", "Fallen Leaves", "Red Flag", "Surrender", "Rusted from the Rain", "Devil in a Midnight Mass", "Devil on My Shoulder", "Highway To Hell" };

        public static List<string> authorsTabPopRestart = new List<string> { "Trzeci Wymiar", "Jeden Osiem L", "Kelly Clarkson", "Borysewicz & Kukiz", "Blue Cafe", "Ewelina Flinta", "Kayah", "Las Ketchup", "Ich Troje", "Ich Troje", "Brathanki", "Tom Jones", "Geri Halliwell", "Jennifer Lopez", "Jennifer Lopez", "Bomfunk MC'S", "Bruno Mars", "Bruno Mars", "Bruno Mars", "Justin Bieber", "Justin Bieber", "Justin Bieber", "Justin Bieber", "Luis Fonsi", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Black Eyed Peas", "Black Eyed Peas", "Black Eyed Peas", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Beyoncé", "Beyoncé", "Justin Timberlake", "Justin Timberlake", "Nelly Furtado", "Nelly Furtado", "Nelly Furtado", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Shawn Mendes", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira" };
        public static List<string> songsTabPopRestart = new List<string> { "Dla mnie masz stajla", "Jak Zapomnieć", "Becouse Of You", "Bo tutaj jest jak jest", "Do Nieba", "Żałuje", "Testosteron", "Asereje", "Powiedz", "Zawsze z Tobą chciałbym być", "W Kinie w Lublinie", "Sexbomb", "It's Raining Men", "Waiting for the night", "Let's Get Loud", "Freestyler", "When I was your Man", "Uptown Funk", "Lazy Song", "Stay", "Love Yourself", "Baby", "Sorry", "Despacito", "Bo jesteś Ty", "Chciałem być", "Za Tobą pójdę jak na bal", "Parostatek", "Zatańczysz ze mną jeszcze raz", "Ale jazz!", "Szampan", "No sory", "ten Stan", "Melodia", "2:00", "GIRL LIKE ME", "Pump It", "I Gotta Feeling", "Payphone ", "Animals", "Sugar", "One More Night", "Moves Like Jagger", "Single Ladies", "Halo", "Mirrors", "Can't Stop The Feeling", "Say It Right", "Maneater ", "Promiscuous", "Umbrella", "Only Girl (In the World)", "Disturbia", "Don't Stop the Music", "Diamonds", "Love The Way You Lie", "Roar", "E.T.", "Last Friday Night (T.G.I.F.)", "I Kissed a Girl", "Hot n Cold", "California Gurls", "Dark Horse", "Firework", "Hit Me Baby One More Time", "Oops!...I Did It Again", "I Wanna Go", "Toxic", "Womanizer", "Bad Romance", "Poker Face", "Paparazzi", "Applause", "Alejandro", "Slipping Through My Fingers", "Lay All Your Love On Me", "Waterloo", "Mamma Mia", "Dancing Queen", "Gimmie! Gimmie! Gimmie!", "Money, Money, Money", "Billie Jean", "Beat It", "Smooth Criminal", "Thriller", "Black Or White", "They Don't Care About Us", "Galway Girl", "I See Fire", "Bad Habits", "Perfect", "Shape Of You", "Stiches", "Idzie Zima", "Niemożliwe", "Dziś późno pójdę spać", "Wzięli zamknęli mi klub", "Loca", "Waka waka", "Rabiosa", "Can't remember to forget you", "Hips Don't Lie", "She Wolf", "Whenever, Wherever" };

        public static List<string> songsFromGame = new List<string>();   
        public Game()
        {
          
            InitializeComponent();


            // Subscribe to the ReadingChanged event to receive accelerometer data updates
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            ShowGame();
               
            
            


        }

        public void Dispose()
        {
            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
        }
        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var acceleration = e.Reading.Acceleration;
            var x = acceleration.X;
            var y = acceleration.Y;
            var z = acceleration.Z;

            // dobrze x < 0.6 z > 0.6    zle z<0.6  x<0.9

            if (answered == false)
            {
                if (x <= 0.6 && y < 0.2 && z > 0.6)
                {
                    BackgroundImageSource = "green.jpg";
                    SongTitle.Text = "Dobrze";
                    Time.IsVisible = false;
                    SongAuthor.IsVisible = false;
                    WrongAnswearButton.IsEnabled = false;
                    GoodAnswearButton.IsEnabled = false;
                    endOfQuestion = true;
                    goodAnswer = true;
                    answered = true;
                }
                else if (z < -0.6 && x < 0.9)
                {
                    BackgroundImageSource = "red.jpg";
                    SongTitle.Text = "Brak odpowiedzi";
                    Time.IsVisible = false;
                    SongAuthor.IsVisible = false;
                    WrongAnswearButton.IsEnabled = false;
                    GoodAnswearButton.IsEnabled = false;
                    endOfQuestion = true;
                    answered = true;
                }
               
            }
            if (x >= 1 && y < 0.2 && z < 0.2)
            {
                answered = false;
            }

        }
        void GameType()
        {
            
            if (MainPage.gameMode == "allSongs")
            {
                StartGame(authorsTab, songsTab, authorsTabRestart, songsTabRestart);
            }
            else if (MainPage.gameMode == "Disney")
            {
                StartGame(authorsTabFairyTales, songsTabFairyTales, authorsTabFairyTalesRestart, songsTabFairyTalesRestart);
            }
            else if (MainPage.gameMode == "Pop")
            {
                StartGame(authorsTabPop, songsTabPop, authorsTabPopRestart, songsTabPopRestart);
            }
            else if (MainPage.gameMode == "Rock")
            {
                StartGame(authorsTabRock, songsTabRock, authorsTabRockRestart, songsTabRockRestart);
            }
            else if (MainPage.gameMode == "UsersMusic")
            {
                StartGame(authorsTabFairyTales, songsTabFairyTales, authorsTabFairyTalesRestart, songsTabFairyTalesRestart);
            }
            else if (MainPage.gameMode == "Rap")
            {
                StartGame(authorsTabRap, songsTabRap, authorsTabRapRestart, songsTabRapRestart);
            }
        }
        public void ShowGame()
        {
            SongAuthor.IsVisible = true;
            Time.IsVisible = true;
            SongTitle.IsVisible = true;
            Task modifyTaskOne = Task.Run(() => GameType());
        }

      


        private async void StartGame(List<string> authorsList, List<string> songsList, List<string> authorsListReset, List<string> songsListReset)
        {
            songsFromGame.Clear();
            int gameCounter = 10;
            Random r = new Random();

            while(gameCounter > 0)
            {
               
                newGame = true;
                songId = r.Next(authorsList.Count);

                if (songId<1)
                {
                    authorsList = authorsListReset;
                    songsList = songsListReset;
                }
                seconds = BeforeGamePage.timeChanger + 1;
                while (newGame == true)
                {
                    Device.BeginInvokeOnMainThread(async() =>
                    {
                        WrongAnswearButton.IsEnabled = true;
                        GoodAnswearButton.IsEnabled = true;
                        SongAuthor.IsVisible = true;
                        Time.IsVisible = true;
                        BackgroundImageSource = "blue.jpg";
                        SongAuthor.Text = authorsList[songId];
                        SongTitle.Text = songsList[songId];
                        seconds--;
                        Time.Text = seconds.ToString();
                        if (seconds<1)
                        {
                            endOfQuestion = true;
                        }
                        if (endOfQuestion == true)
                        {
                            SongAuthor.IsVisible = false;
                            Time.IsVisible = false;
                            newGame = false;
                            songsFromGame.Add(songsList[songId]);
                            gameCounter--;
                            authorsList.RemoveAt(songId);
                            songsList.RemoveAt(songId);
                            if (goodAnswer == true)
                            {
                                endOfQuestion = false;
                                pointsCounter++;
                                goodAnswer = false;
                                BackgroundImageSource = "green.jpg";
                                SongTitle.Text = "Dobrze";
                            }
                            else
                            {
                                endOfQuestion = false;
                                BackgroundImageSource = "red.jpg";
                                SongTitle.Text = "Brak odpowiedzi";
                            }
                            if (gameCounter == 0)
                            {
                                Dispose();
                                Accelerometer.Stop();
                                await Navigation.PushAsync(new AfterGame());
                            }
                        }

                    });
                    Thread.Sleep(1000);


                }
            }



        }
        private void WrongAnswearButton_Clicked(object sender, EventArgs e)
        {
            BackgroundImageSource = "red.jpg";
            SongTitle.Text = "Brak odpowiedzi";
            Time.IsVisible = false;
            SongAuthor.IsVisible = false;
            WrongAnswearButton.IsEnabled = false;
            GoodAnswearButton.IsEnabled = false;
            endOfQuestion = true;
           
            
        }

        private void GoodAnswearButton_Clicked(object sender, EventArgs e)
        {
            BackgroundImageSource = "green.jpg";
            SongTitle.Text = "Dobrze";
            Time.IsVisible = false;
            SongAuthor.IsVisible = false;
            WrongAnswearButton.IsEnabled = false;
            GoodAnswearButton.IsEnabled = false;
            endOfQuestion = true;
            pointsCounter++;
            goodAnswer = true; 
         
        }
       
    }
}