﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;
using System.IO;
using Xamarin.Essentials;

namespace JakaToPiosenka
{
    public partial class MainPage : ContentPage
    {
        int seconds = 35;
        int secondsValue = 32;
        bool newGame = false;
        int pointsCounter = 0;
        int lvlCounter = 0;
        int timeLengthChanger = 2;
        int typeOfSongsChanger = 1;
        int backButtonChanger = 1;
        bool firstImplementSongs = false;

        int songId;
        int songFairyTaleId;
        int songRockId;
        int songPopId;
        int songRapId;

        int iAllSongs = 0;
        int iPopSongs = 0;
        int iRockSongs = 0;
        int iFairyTalesSongs = 0;

        List<string> authorsTab = new List<string> { "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Myslovitz", "Myslovitz", "Myslovitz", "Happysad", "Republika", "Republika", "Wilki", "Wilki", "Kombi", "Urszula", "Urszula", "Luxtorpeda", "Maanam", "Maanam", "Czerwone Gitary", "Kobranocka", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Czesław Niemen", "Dżem", "Dżem", "Budka Suflera", "Budka Suflera", "Budka Suflera", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Black Eyed Peas", "Black Eyed Peas", "Black Eyed Peas", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Beyoncé", "Beyoncé", "Justin Timberlake", "Justin Timberlake", "Nelly Furtado", "Nelly Furtado", "Nelly Furtado", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Bob Dylan", "Rammstein", "Rammstein", "Rammstein", "Oasis", "Panic! at the Disco", "Panic! at the Disco", "Panic! at the Disco", "Pink Floyd", "Pink Floyd", "Red Hot Chili Peppers", "Bon Jovi", "The Cranberries", "Metallica", "Metallica", "Metallica", "Guns N' Roses", "Guns N' Roses", "The Doors", "System of a Down", "System of a Down", "System of a Down", "Green Day", "Green Day", "Green Day", "Green Day", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Nirvana", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Enej", "Enej", "Enej", "Enej", "Dawid Podsiadło", "Dawid Podsiadło", "Dawid Podsiadło", "Dawid Podsiadło", "Carly Ray Japsen", "Avicii", "Avicii", "Avicii", "Kazik Staszewski", "Kazik Staszewski", "Kazik Staszewski", "Kazik Staszewski", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "AC/DC", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Shawn Mendes", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira" };
        List<string> songsTab = new List<string> { "Bo jesteś Ty", "Chciałem być", "Za Tobą pójdę jak na bal", "Parostatek", "Zatańczysz ze mną jeszcze raz", "Ale jazz!", "Szampan", "No sory", "ten Stan", "Melodia", "2:00", "Długość dźwięku samotności", "Scenariusz dla moich sąsiadów", "Peggy Brown", "Zanim pójdę", "Mamona", "Telefony", "Nie Stało Się Nic", "Baśka", "Słodkiego miłego życia", "Koń na Biegunach", "Dmuchawce, latawce", "Autystyczny", "Cykady na Cykladach", "Kocham Cię, kochanie moje", "Ciągle pada", "Kocham Cię jak Irlandię", "Zawsze Tam Gdzie Ty", "Kryzysowa Narzeczona", "Tańcz głupia, tańcz", "Mniej niż Zero", "Stacja Warszawa", "Dziwny jest ten świat", "Wehikuł czasu", "Whisky", "Jolka, Jolka pamiętasz", "Takie Tango", "Bal wszystkich świętych", "Nie płacz Ewka", "Wszystko ma swój czas ", "Chcemy być sobą", "Autobiografia ", "Kołysanka dla nieznajomej", "Ale w koło jest wesoło", "GIRL LIKE ME", "Pump It", "I Gotta Feeling", "Payphone ", "Animals", "Sugar", "One More Night", "Moves Like Jagger", "Single Ladies", "Halo", "Mirrors", "Can't Stop The Feeling", "Say It Right", "Maneater ", "Promiscuous", "Umbrella", "Only Girl (In the World)", "Disturbia", "Don't Stop the Music", "Diamonds", "Love The Way You Lie", "Roar", "E.T.", "Last Friday Night (T.G.I.F.)", "I Kissed a Girl", "Hot n Cold", "California Gurls", "Dark Horse", "Firework", "Hit Me Baby One More Time", "Oops!...I Did It Again", "I Wanna Go", "Toxic", "Womanizer", "Bad Romance", "Poker Face", "Paparazzi", "Applause", "Alejandro", "Knockin' on heaven's door", "DEUTSCHLAND", "Du Hast", "Sonne", "Wonderwall ", "Girls / Girls / Boys", "High Hopes", "Let's Kill Tonight", "High Hopes", "Another brick in the wall part II", "Californication", "It's My Life", "Zombie", "Nothing Else Matters", "Enter Sandman", "Master of Puppets", "Sweet Child O' Mine", "Knockin' On Heaven's Door", "Riders On The Storm", "Chop Suey", "Aerials", "Lonely Day", "Boulevard of Broken Dreams", "Holiday", "Wake Me Up When September Ends", "American Idiot", "Numb", "What I've Done", "Castle of Glas", "In The End", "Somewhere I Belong", "Smells Like Teen Spirit", "Slipping Through My Fingers", "Lay All Your Love On Me", "Waterloo", "Mamma Mia", "Dancing Queen", "Gimmie! Gimmie! Gimmie!", "Money, Money, Money", "Billie Jean", "Beat It", "Smooth Criminal", "Thriller", "Black Or White", "They Don't Care About Us", "Good Old-Fashioned Lover Boy", "Killer Queen", "Love of My Life", "The Show Must Go On", "Radio Ga Ga", "I Want to Break Free", "Don't Stop Me Now", "Bohemian Rhapsody", "We Will Rock You", "Please Mr. Postman", "Twist and Shout", "I Want To Hold Your Hand", "Let It Be", "Hey Jude", "Yesterday", "Here Comes the Sun", "Skrzydlate ręce", "Tak smakuje życie", "Radio Hello", "Lili", "Małomiasteczkowy", "Nie Ma Fal", "Trójkąty i Kwadraty", "W dobrą stronę", "Call me mayby", "You Make Me", "Hey Brother", "Wake me up", "Arahja", "Baranek", "Gdy nie ma dzieci", "12 Groszy", "Fallen Leaves", "Red Flag", "Surrender", "Rusted from the Rain", "Devil in a Midnight Mass", "Devil on My Shoulder", "Highway To Hell", "Galway Girl", "I See Fire", "Bad Habits", "Shivers", "Perfect", "Shape Of You", "Stiches", "Idzie Zima", "Niemożliwe", "Dziś późno pójdę spać", "Wzięli zamknęli mi klub", "Loca", "Waka Waka", "Hips Don't Lie", "She Wolf", "Whenever, Wherever", "Rabiosa", "Can't remember to forget you" };

        List<string> authorsTabFairyTales = new List<string> { "Kurczak Mały", "Smerfy", "Reksio", "Scooby Doo", "Nowe Przygody Kubusia Puchatka", "Gumisie", "Kacze Opowieści", "Chip i Dale", "Coco", "Coco", "Coco", "Coco", "Madagaskar", "Pingwiny z Madagaskaru", "Toy Story", "Planeta Skarbów", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Herkules", "Herkules", "Herkules", "Herkules", "Herkules", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Mulan", "Mulan", "Mulan", "Mulan", "Mulan", "Mulan", "Tarzan", "Tarzan", "Tarzan", "Tarzan", "Piękna i Bestia", "Piękna i Bestia", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Aladdyn", "Aladdyn", "Aladdyn", "Aladdyn", "Zaplątani", "Zaplątani", "Zaplątani", "Zaplątani", "Księżniczka Łabędzi", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Rogate Ranczo", "Rogate Ranczo", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu 2", "Kraina Lodu 2", "Kraina Lodu 2", "Merida Waleczna", "Barbie Księżniczka i Żebraczka", "Barbie Księżniczka i Żebraczka", "Barbie Księżniczka i Żebraczka", "Lilo i Stitch", "Lilo i Stitch", "Lilo i Stitch", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Vaiana", "Vaiana", "Vaiana", "Vaiana", "Pocahontas", "Pocahontas", "Pocahontas" };
        List<string> songsTabFairyTales = new List<string> { "Ten mały Błąd", "Intro", "Intro", "Intro", "Intro", "Intro", "Intro", "Intro", "La Llorona", "Corazón", "Pamiętaj Mnie", "Un Poco Loco", "Wyginam śmiało ciało", "Ja i mój JJ", "Ty Druha we mnie masz", "Jestem Kimś", "Chcę być Tam", "Jaskinia Cudów", "Niezwykły Gość", "Na Odwyrtkę", "Z Dna Piekieł", "Modlitwa Esmeraldy", "Dźwięk Dzwonów Notre Dame", "Aleja Gwiazd", "Zaprawdę Wierzcie Mi", "Droga Mi Nie Straszna", "Straciłem Nadzieję", "Zero To Hero", "Upendi", "Miłość Drogę Zna", "Jeden Głos", "Duch Żyje w Nas", "Hakuna Matata", "Miłość Rośnie wokół Nas", "Strasznie już być Tym Królem chcę", "Krąg życia", "Taka jak inne pragnę być", "O tym lekcja ta", "Za którą walczyć chcesz", "Zrobię Mężczyzn z Was", "Lustro", "Zaszczyt Nam przyniesie To", "W Mym Sercu Twój Dom", "Obcy tacy jak Ja", "Człowieka Syn", "Światy Dwa", "Gaston", "Gościem Bądź", "Chciałabym być tam, gdzie ludzie są", "Całuj Ją", "Na Morza Dnie", "Przez te chwilę czuję, że", "Morze i stały Ląd", "Tańcz Seniora", "O Krok", "Nie ma takich dwóch", "Książe Ali", "Wspaniały Świat", "Cierpliwie Czekam", "Marzenie Mam", "Po raz pierwszy widzę Blask", "Nowe Dni", "Na dłużej niż na zawsze", "Transformacja", "Bratu Równy Brat", "Już wyruszać Czas", "I Ty możesz zostać z Nami", "Już Nikt już Nic", "To własnie Dom", "Na południu w Luizianie", "Przyjaciele z Zaświatów", "Prawie udało się", "Poszukaj Głębiej", "W Drodze Przez Bajoro", "Skrawek Nieba Mam", "Yodle-Adle-Eedle-Idle-Oo", "To co ukochałem", "Uwolnij Nas", "Przez Nieba Wzrok", "Nie z Patałachami grasz", "Plagi", "Cuda dzieją się", "Ulepimy Dziś Bałwana", "Miłość stanęła w drzwiach", "Pierwszy raz jak sięga Pamięć", "Lód w Lecie", "Mam Tę Moc", "Nie Ten Tego", "Pokaż się", "Chcę uwierzyć Snom", "Którą wybrać Mam z Dróg", "Chwytam Wiatr", "Chcę naprawdę wolną być", "Ja tak jak Ty", "Jesteś koto-psem", "He Mele No Lilo", "Hawaiian Roller Coaster", "Burning Love", "Długo i Szczęśliwie", "O Krok", "Skąd wiedzieć ma", "Pracować będzie lżej", "O miłosnym Pocałunku śnię", "Będę wracał", "Złaź ze Mnie no już", "Chcę Wolnym Być", "Oto Ja", "Pół Kroku Stąd", "Na Drodze Tej", "Drobnostka", "Błyszczeć", "Kolorowy Wiatr", "Ten za Łukiem Rzeki Świat", "Dzicy Są!" };

        List<string> authorsTabRock = new List<string> { "Golden Life", "Eric Clapton", "Myslovitz", "Myslovitz", "Myslovitz", "Happysad", "Republika", "Republika", "Wilki", "Wilki", "Kombii", "Urszula", "Urszula", "Luxtorpeda", "Maanam", "Maanam", "Czerwone Gitary", "Kobranocka", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Czesław Niemen", "Dżem", "Dżem", "Budka Suflera", "Budka Suflera", "Budka Suflera", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Bob Dylan", "Rammstein", "Rammstein", "Rammstein", "Oasis", "Panic! at the Disco", "Panic! at the Disco", "Panic! at the Disco", "Pink Floyd", "Pink Floyd", "Red Hot Chili Peppers", "Bon Jovi", "The Cranberries", "Metallica", "Metallica", "Metallica", "Guns N' Roses", "Guns N' Roses", "The Doors", "System of a Down", "System of a Down", "System of a Down", "Green Day", "Green Day", "Green Day", "Green Day", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Nirvana", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "AC/DC" };
        List<string> songsTabRock = new List<string> { "Oprócz(błękitnego nieba)", "Tears in Heaven", "Długość dźwięku samotności", "Scenariusz dla moich sąsiadów", "Peggy Brown", "Zanim pójdę", "Mamona", "Telefony", "Nie Stało Się Nic", "Baśka", "Słodkiego miłego życia", "Koń na Biegunach", "Dmuchawce, latawce", "Autystyczny", "Cykady na Cykladach", "Kocham Cię, kochanie moje", "Ciągle pada", "Kocham Cię jak Irlandię", "Zawsze Tam Gdzie Ty", "Kryzysowa Narzeczona", "Tańcz głupia, tańcz", "Mniej niż Zero", "Stacja Warszawa", "Dziwny jest ten świat", "Wehikuł czasu", "Whisky", "Jolka, Jolka pamiętasz", "Takie Tango", "Bal wszystkich świętych", "Nie płacz Ewka", "Wszystko ma swój czas ", "Chcemy być sobą", "Autobiografia ", "Kołysanka dla nieznajomej", "Ale w koło jest wesoło", "Knockin' on heaven's door", "DEUTSCHLAND", "Du Hast", "Sonne", "Wonderwall ", "Girls / Girls / Boys", "High Hopes", "Let's Kill Tonight", "High Hopes", "Another brick in the wall", "Californication", "It's My Life", "Zombie", "Nothing Else Matters", "Enter Sandman", "Master of Puppets", "Sweet Child O' Mine", "Knockin' On Heaven's Door", "Riders On The Storm", "Chop Suey", "Aerials", "Lonely Day", "Boulevard of Broken Dreams", "Holiday", "Wake Me Up When September Ends", "American Idiot", "Numb", "What I've Done", "Castle of Glas", "In The End", "Somewhere I Belong", "Smells Like Teen Spirit", "Good Old-Fashioned Lover Boy", "Killer Queen", "Love of My Life", "The Show Must Go On", "Radio Ga Ga", "I Want to Break Free", "Don't Stop Me Now", "Bohemian Rhapsody", "We Will Rock You", "Please Mr. Postman", "Yesterday", "Twist and Shout", "I Want To Hold Your Hand", "Let It Be", "Hey Jude", "Here Comes the Sun", "Fallen Leaves", "Red Flag", "Surrender", "Rusted from the Rain", "Devil in a Midnight Mass", "Devil on My Shoulder", "Highway To Hell" };

        List<string> authorsTabPop = new List<string> { " CHARLES & EDDIE", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "Dr.Alban", "Mr.Big", " CHARLES & EDDIE", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Black Eyed Peas", "Black Eyed Peas", "Black Eyed Peas", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Beyoncé", "Beyoncé", "Justin Timberlake", "Justin Timberlake", "Nelly Furtado", "Nelly Furtado", "Nelly Furtado", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Shawn Mendes", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira" };
        List<string> songsTabPop = new List<string>   { "Would I Lie To You?", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "costam", "It's my life","To be with You", "Would I Lie To You?", "Bo jesteś Ty", "Chciałem być", "Za Tobą pójdę jak na bal", "Parostatek", "Zatańczysz ze mną jeszcze raz", "Ale jazz!", "Szampan", "No sory", "ten Stan", "Melodia", "2:00", "GIRL LIKE ME", "Pump It", "I Gotta Feeling", "Payphone ", "Animals", "Sugar", "One More Night", "Moves Like Jagger", "Single Ladies", "Halo", "Mirrors", "Can't Stop The Feeling", "Say It Right", "Maneater ", "Promiscuous", "Umbrella", "Only Girl (In the World)", "Disturbia", "Don't Stop the Music", "Diamonds", "Love The Way You Lie", "Roar", "E.T.", "Last Friday Night (T.G.I.F.)", "I Kissed a Girl", "Hot n Cold", "California Gurls", "Dark Horse", "Firework", "Hit Me Baby One More Time", "Oops!...I Did It Again", "I Wanna Go", "Toxic", "Womanizer", "Bad Romance", "Poker Face", "Paparazzi", "Applause", "Alejandro", "Slipping Through My Fingers", "Lay All Your Love On Me", "Waterloo", "Mamma Mia", "Dancing Queen", "Gimmie! Gimmie! Gimmie!", "Money, Money, Money", "Billie Jean", "Beat It", "Smooth Criminal", "Heal The World", "Thriller", "Black Or White", "They Don't Care About Us", "Galway Girl", "I See Fire", "Bad Habits", "Perfect", "Shape Of You", "Stiches", "Idzie Zima", "Niemożliwe", "Dziś późno pójdę spać", "Wzięli zamknęli mi klub", "Loca", "Waka waka", "Rabiosa", "Can't remember to forget you", "Hips Don't Lie", "She Wolf", "Whenever, Wherever" };

        List<string> authorsTabRap = new List<string> { "Twenty one pilots", "Twenty one pilots", "White 2115", "White 2115", "White 2115", "Tymek", "Tymek", "Tymek", "Tymek", "Kizo feat. Malik Montana", "Malik Montana", "Malik Montana", "Żabson", "Żabson", "Żabson", "Żabson", "Kizo", "Kizo", "Kizo", "Kizo", "Kizo", "Kizo", "Sobel", "Sobel", "Sobel", "Sobel", "Sobel", "Mata", "Mata", "Mata", "Mata", "Mata", "Mata", "Mata", "Kuban", "Kuban", "Kuban", "Kuban", "Kuban", "PRO8L3M", "PRO8L3M", "PRO8L3M", "PRO8L3M", "PRO8L3M", "TACONAFIDE", "TACONAFIDE", "TACONAFIDE", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Wiz Khalifa", "Wiz Khalifa", "Wiz Khalifa", "Kendrick Lamar", "Kendrick Lamar", "Kendrick Lamar", "Macklemore", "Macklemore", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem" };
        List<string> songsTabRap = new List<string> { "Ride", "Stressed Out", "Morgan", "California", "Mogę dziś umierać", "Język ciała", "Anioły i Demony", "Rainman", "Poza kontrolą", "CZEMPION", "1szy Nos", "Naaajak", "Puerto Bounce", "Do Ziomów", "Księżniczki", "Sexoholik", "Miasto 24H", "Toskania", "Fitness", "Lucky Punch", "Disney", "Kizo", "Impreza", "Testarossa", "Bandyta", "Fiołkowe Pole", "Cześć, jak się masz?", "100 dni do matury", "Patointeligencja", "Mata Montana", "Schodki", "Kiss cam", "Papuga", "Patoreakcja", "Suki", "DOBzI LUDZIE", "Ta dama", "W taki dzień", "Chore jazdy", "Stówa", "TEB 200-1", "Flary", "Molly", "Skrable", "Kryptowaluty", "8 kobiet", "Tamagotchi", "Nostalgia", "Fiji", "Następna stacja", "6 zer", "Deszcz na betonie", "We Own It", "We Dem Boyz", "Black and Yellow", "Swimming Pools", "Alright", "HUMBLE", "Can't Hold Us", "Trift Shop", "Shake That", "Like Toy Soldiers", "Beautiful", "Just Lose It", "Not Afraid", "When I'm Gone", "Mockingbird", "Rap God", "The Real Slim Shady", "Without Me" };

        List<string> authorsTabKoledy = new List<string> { };
        List<string> songsTabKoledy = new List<string> { };


        List<string> authorsTabRapRestart = new List<string> { "Twenty one pilots", "Twenty one pilots", "White 2115", "White 2115", "White 2115", "Tymek", "Tymek", "Tymek", "Tymek", "Kizo feat. Malik Montana", "Malik Montana", "Malik Montana", "Żabson", "Żabson", "Żabson", "Żabson", "Kizo", "Kizo", "Kizo", "Kizo", "Kizo", "Kizo", "Sobel", "Sobel", "Sobel", "Sobel", "Sobel", "Mata", "Mata", "Mata", "Mata", "Mata", "Mata", "Mata", "Kuban", "Kuban", "Kuban", "Kuban", "Kuban", "PRO8L3M", "PRO8L3M", "PRO8L3M", "PRO8L3M", "PRO8L3M", "TACONAFIDE", "TACONAFIDE", "TACONAFIDE", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Taco Hemingway", "Wiz Khalifa", "Wiz Khalifa", "Wiz Khalifa", "Kendrick Lamar", "Kendrick Lamar", "Kendrick Lamar", "Macklemore", "Macklemore", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem", "Eminem" };
        List<string> songsTabRapRestart = new List<string> { "Ride", "Stressed Out", "Morgan", "California", "Mogę dziś umierać", "Język ciała", "Anioły i Demony", "Rainman", "Poza kontrolą", "CZEMPION", "1szy Nos", "Naaajak", "Puerto Bounce", "Do Ziomów", "Księżniczki", "Sexoholik", "Miasto 24H", "Toskania", "Fitness", "Lucky Punch", "Disney", "Kizo", "Impreza", "Testarossa", "Bandyta", "Fiołkowe Pole", "Cześć, jak się masz?", "100 dni do matury", "Patointeligencja", "Mata Montana", "Schodki", "Kiss cam", "Papuga", "Patoreakcja", "Suki", "DOBzI LUDZIE", "Ta dama", "W taki dzień", "Chore jazdy", "Stówa", "TEB 200-1", "Flary", "Molly", "Skrable", "Kryptowaluty", "8 kobiet", "Tamagotchi", "Nostalgia", "Fiji", "Następna stacja", "6 zer", "Deszcz na betonie", "We Own It", "We Dem Boyz", "Black and Yellow", "Swimming Pools", "Alright", "HUMBLE", "Can't Hold Us", "Trift Shop", "Shake That", "Like Toy Soldiers", "Beautiful", "Just Lose It", "Not Afraid", "When I'm Gone", "Mockingbird", "Rap God", "The Real Slim Shady", "Without Me" };


        List<string> authorsTabRestart = new List<string> { "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Myslovitz", "Myslovitz", "Myslovitz", "Happysad", "Republika", "Republika", "Wilki", "Wilki", "Kombi", "Urszula", "Urszula", "Luxtorpeda", "Maanam", "Maanam", "Czerwone Gitary", "Kobranocka", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Czesław Niemen", "Dżem", "Dżem", "Budka Suflera", "Budka Suflera", "Budka Suflera", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Black Eyed Peas", "Black Eyed Peas", "Black Eyed Peas", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Beyoncé", "Beyoncé", "Justin Timberlake", "Justin Timberlake", "Nelly Furtado", "Nelly Furtado", "Nelly Furtado", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Bob Dylan", "Rammstein", "Rammstein", "Rammstein", "Oasis", "Panic! at the Disco", "Panic! at the Disco", "Panic! at the Disco", "Pink Floyd", "Pink Floyd", "Red Hot Chili Peppers", "Bon Jovi", "The Cranberries", "Metallica", "Metallica", "Metallica", "Guns N' Roses", "Guns N' Roses", "The Doors", "System of a Down", "System of a Down", "System of a Down", "Green Day", "Green Day", "Green Day", "Green Day", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Nirvana", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Enej", "Enej", "Enej", "Enej", "Dawid Podsiadło", "Dawid Podsiadło", "Dawid Podsiadło", "Dawid Podsiadło", "Carly Ray Japsen", "Avicii", "Avicii", "Avicii", "Kazik Staszewski", "Kazik Staszewski", "Kazik Staszewski", "Kazik Staszewski", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "AC/DC", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Shawn Mendes", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira" };
        List<string> songsTabRestart = new List<string> { "Bo jesteś Ty", "Chciałem być", "Za Tobą pójdę jak na bal", "Parostatek", "Zatańczysz ze mną jeszcze raz", "Ale jazz!", "Szampan", "No sory", "ten Stan", "Melodia", "2:00", "Długość dźwięku samotności", "Scenariusz dla moich sąsiadów", "Peggy Brown", "Zanim pójdę", "Mamona", "Telefony", "Nie Stało Się Nic", "Baśka", "Słodkiego miłego życia", "Koń na Biegunach", "Dmuchawce, latawce", "Autystyczny", "Cykady na Cykladach", "Kocham Cię, kochanie moje", "Ciągle pada", "Kocham Cię jak Irlandię", "Zawsze Tam Gdzie Ty", "Kryzysowa Narzeczona", "Tańcz głupia, tańcz", "Mniej niż Zero", "Stacja Warszawa", "Dziwny jest ten świat", "Wehikuł czasu", "Whisky", "Jolka, Jolka pamiętasz", "Takie Tango", "Bal wszystkich świętych", "Nie płacz Ewka", "Wszystko ma swój czas ", "Chcemy być sobą", "Autobiografia ", "Kołysanka dla nieznajomej", "Ale w koło jest wesoło", "GIRL LIKE ME", "Pump It", "I Gotta Feeling", "Payphone ", "Animals", "Sugar", "One More Night", "Moves Like Jagger", "Single Ladies", "Halo", "Mirrors", "Can't Stop The Feeling", "Say It Right", "Maneater ", "Promiscuous", "Umbrella", "Only Girl (In the World)", "Disturbia", "Don't Stop the Music", "Diamonds", "Love The Way You Lie", "Roar", "E.T.", "Last Friday Night (T.G.I.F.)", "I Kissed a Girl", "Hot n Cold", "California Gurls", "Dark Horse", "Firework", "Hit Me Baby One More Time", "Oops!...I Did It Again", "I Wanna Go", "Toxic", "Womanizer", "Bad Romance", "Poker Face", "Paparazzi", "Applause", "Alejandro", "Knockin' on heaven's door", "DEUTSCHLAND", "Du Hast", "Sonne", "Wonderwall ", "Girls / Girls / Boys", "High Hopes", "Let's Kill Tonight", "High Hopes", "Another brick in the wall part II", "Californication", "It's My Life", "Zombie", "Nothing Else Matters", "Enter Sandman", "Master of Puppets", "Sweet Child O' Mine", "Knockin' On Heaven's Door", "Riders On The Storm", "Chop Suey", "Aerials", "Lonely Day", "Boulevard of Broken Dreams", "Holiday", "Wake Me Up When September Ends", "American Idiot", "Numb", "What I've Done", "Castle of Glas", "In The End", "Somewhere I Belong", "Smells Like Teen Spirit", "Slipping Through My Fingers", "Lay All Your Love On Me", "Waterloo", "Mamma Mia", "Dancing Queen", "Gimmie! Gimmie! Gimmie!", "Money, Money, Money", "Billie Jean", "Beat It", "Smooth Criminal", "Thriller", "Black Or White", "They Don't Care About Us", "Good Old-Fashioned Lover Boy", "Killer Queen", "Love of My Life", "The Show Must Go On", "Radio Ga Ga", "I Want to Break Free", "Don't Stop Me Now", "Bohemian Rhapsody", "We Will Rock You", "Please Mr. Postman", "Twist and Shout", "I Want To Hold Your Hand", "Let It Be", "Hey Jude", "Yesterday", "Here Comes the Sun", "Skrzydlate ręce", "Tak smakuje życie", "Radio Hello", "Lili", "Małomiasteczkowy", "Nie Ma Fal", "Trójkąty i Kwadraty", "W dobrą stronę", "Call me mayby", "You Make Me", "Hey Brother", "Wake me up", "Arahja", "Baranek", "Gdy nie ma dzieci", "12 Groszy", "Fallen Leaves", "Red Flag", "Surrender", "Rusted from the Rain", "Devil in a Midnight Mass", "Devil on My Shoulder", "Highway To Hell", "Galway Girl", "I See Fire", "Bad Habits", "Shivers", "Perfect", "Shape Of You", "Stiches", "Idzie Zima", "Niemożliwe", "Dziś późno pójdę spać", "Wzięli zamknęli mi klub", "Loca", "Waka Waka", "Hips Don't Lie", "She Wolf", "Whenever, Wherever", "Rabiosa", "Can't remember to forget you" };

        List<string> authorsTabFairyTalesRestart = new List<string> { "Kurczak Mały", "Smerfy", "Reksio", "Scooby Doo", "Nowe Przygody Kubusia Puchatka", "Gumisie", "Kacze Opowieści", "Chip i Dale", "Coco", "Coco", "Coco", "Coco", "Madagaskar", "Pingwiny z Madagaskaru", "Toy Story", "Planeta Skarbów", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Dzwonnik z Notre Dame", "Herkules", "Herkules", "Herkules", "Herkules", "Herkules", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Król Lew", "Mulan", "Mulan", "Mulan", "Mulan", "Mulan", "Mulan", "Tarzan", "Tarzan", "Tarzan", "Tarzan", "Piękna i Bestia", "Piękna i Bestia", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Mała Syrenka", "Aladdyn", "Aladdyn", "Aladdyn", "Aladdyn", "Zaplątani", "Zaplątani", "Zaplątani", "Zaplątani", "Księżniczka Łabędzi", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Mój Brat Niedźwiedź", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Księżniczka i Żaba", "Rogate Ranczo", "Rogate Ranczo", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Książe Egiptu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu", "Kraina Lodu 2", "Kraina Lodu 2", "Kraina Lodu 2", "Merida Waleczna", "Barbie Księżniczka i Żebraczka", "Barbie Księżniczka i Żebraczka", "Barbie Księżniczka i Żebraczka", "Lilo i Stitch", "Lilo i Stitch", "Lilo i Stitch", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Zaczarowana", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Mustang z Dzikiej Doliny", "Vaiana", "Vaiana", "Vaiana", "Vaiana", "Pocahontas", "Pocahontas", "Pocahontas" };
        List<string> songsTabFairyTalesRestart = new List<string> { "Ten mały Błąd", "Intro", "Intro", "Intro", "Intro", "Intro", "Intro", "Intro", "La Llorona", "Corazón", "Pamiętaj Mnie", "Un Poco Loco", "Wyginam śmiało ciało", "Ja i mój JJ", "Ty Druha we mnie masz", "Jestem Kimś", "Chcę być Tam", "Jaskinia Cudów", "Niezwykły Gość", "Na Odwyrtkę", "Z Dna Piekieł", "Modlitwa Esmeraldy", "Dźwięk Dzwonów Notre Dame", "Aleja Gwiazd", "Zaprawdę Wierzcie Mi", "Droga Mi Nie Straszna", "Straciłem Nadzieję", "Zero To Hero", "Upendi", "Miłość Drogę Zna", "Jeden Głos", "Duch Żyje w Nas", "Hakuna Matata", "Miłość Rośnie wokół Nas", "Strasznie już być Tym Królem chcę", "Krąg życia", "Taka jak inne pragnę być", "O tym lekcja ta", "Za którą walczyć chcesz", "Zrobię Mężczyzn z Was", "Lustro", "Zaszczyt Nam przyniesie To", "W Mym Sercu Twój Dom", "Obcy tacy jak Ja", "Człowieka Syn", "Światy Dwa", "Gaston", "Gościem Bądź", "Chciałabym być tam, gdzie ludzie są", "Całuj Ją", "Na Morza Dnie", "Przez te chwilę czuję, że", "Morze i stały Ląd", "Tańcz Seniora", "O Krok", "Nie ma takich dwóch", "Książe Ali", "Wspaniały Świat", "Cierpliwie Czekam", "Marzenie Mam", "Po raz pierwszy widzę Blask", "Nowe Dni", "Na dłużej niż na zawsze", "Transformacja", "Bratu Równy Brat", "Już wyruszać Czas", "I Ty możesz zostać z Nami", "Już Nikt już Nic", "To własnie Dom", "Na południu w Luizianie", "Przyjaciele z Zaświatów", "Prawie udało się", "Poszukaj Głębiej", "W Drodze Przez Bajoro", "Skrawek Nieba Mam", "Yodle-Adle-Eedle-Idle-Oo", "To co ukochałem", "Uwolnij Nas", "Przez Nieba Wzrok", "Nie z Patałachami grasz", "Plagi", "Cuda dzieją się", "Ulepimy Dziś Bałwana", "Miłość stanęła w drzwiach", "Pierwszy raz jak sięga Pamięć", "Lód w Lecie", "Mam Tę Moc", "Nie Ten Tego", "Pokaż się", "Chcę uwierzyć Snom", "Którą wybrać Mam z Dróg", "Chwytam Wiatr", "Chcę naprawdę wolną być", "Ja tak jak Ty", "Jesteś koto-psem", "He Mele No Lilo", "Hawaiian Roller Coaster", "Burning Love", "Długo i Szczęśliwie", "O Krok", "Skąd wiedzieć ma", "Pracować będzie lżej", "O miłosnym Pocałunku śnię", "Będę wracal", "Złaź ze Mnie no już", "Chcę Wolnym Być", "Oto Ja", "Pół Kroku Stąd", "Na Drodze Tej", "Drobnostka", "Błyszczeć", "Kolorowy Wiatr", "Ten za Łukiem Rzeki Świat", "Dzicy Są!" };

        List<string> authorsTabRockRestart = new List<string> { "Myslovitz", "Myslovitz", "Myslovitz","Happysad","Republika", "Republika","Wilki", "Wilki","Kombii", "Kombii", "Urszula", "Urszula","Luxtorpeda","Maanam", "Maanam","Czerwone Gitary","Kobranocka","Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank", "Lady Pank","Czesław Niemen","Dżem", "Dżem", "Budka Suflera", "Budka Suflera", "Budka Suflera", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect", "Perfect","Bob Dylan", "Rammstein", "Rammstein", "Rammstein", "Oasis", "Panic! at the Disco", "Panic! at the Disco", "Panic! at the Disco", "Pink Floyd", "Pink Floyd", "Red Hot Chili Peppers", "Bon Jovi", "The Cranberries", "Metallica", "Metallica", "Metallica", "Guns N' Roses", "Guns N' Roses", "The Doors", "System of a Down", "System of a Down", "System of a Down", "Green Day", "Green Day", "Green Day", "Green Day", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Linking Park", "Nirvana", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Queen", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Beatles", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "Billy Talent", "AC/DC" };
        List<string> songsTabRockRestart = new List<string>   { "Długość dźwięku samotności", "Scenariusz dla moich sąsiadów", "Peggy Brown", "Zanim pójdę", "Mamona", "Telefony", "Nie Stało Się Nic", "Baśka","Słodkiego miłego życia", " Pokolenie","Koń na Biegunach","Dmuchawce, latawce","Autystyczny","Cykady na Cykladach", "Kocham Cię, kochanie moje", "Ciągle pada","Kocham Cię jak Irlandię","Zawsze Tam Gdzie Ty", "Kryzysowa Narzeczona", "Tańcz głupia, tańcz", "Mniej niż Zero", "Stacja Warszawa", "Dziwny jest ten świat", "Wehikuł czasu", "Whisky",  "Jolka, Jolka pamiętasz", "Takie Tango", "Bal wszystkich świętych",  "Nie płacz Ewka", "Wszystko ma swój czas ", "Chcemy być sobą", "Autobiografia ", "Kołysanka dla nieznajomej", "Ale w koło jest wesoło", "Knockin' on heaven's door", "DEUTSCHLAND", "Du Hast", "Sonne", "Wonderwall ", "Girls / Girls / Boys", "High Hopes", "Let's Kill Tonight", "High Hopes", "Another brick in the wall", "Californication", "It's My Life", "Zombie", "Nothing Else Matters", "Enter Sandman", "Master of Puppets", "Sweet Child O' Mine", "Knockin' On Heaven's Door", "Riders On The Storm", "Chop Suey", "Aerials", "Lonely Day", "Boulevard of Broken Dreams", "Holiday", "Wake Me Up When September Ends", "American Idiot", "Numb", "What I've Done", "Castle of Glas", "In The End", "Somewhere I Belong", "Smells Like Teen Spirit", "Good Old-Fashioned Lover Boy", "Killer Queen", "Love of My Life", "The Show Must Go On", "Radio Ga Ga", "I Want to Break Free", "Don't Stop Me Now", "Bohemian Rhapsody", "We Will Rock You", "Please Mr. Postman", "Yesterday", "Twist and Shout", "I Want To Hold Your Hand", "Let It Be", "Hey Jude", "Here Comes the Sun", "Fallen Leaves", "Red Flag", "Surrender", "Rusted from the Rain", "Devil in a Midnight Mass", "Devil on My Shoulder", "Highway To Hell" };
        
        List<string> authorsTabPopRestart = new List<string> { "Trzeci Wymiar", "Jeden Osiem L", "Kelly Clarkson", "Borysewicz & Kukiz", "Blue Cafe", "Ewelina Flinta", "Kayah", "Las Ketchup","Ich Troje", "Ich Troje", "Brathanki", "Tom Jones", "Geri Halliwell", "Jennifer Lopez", "Jennifer Lopez", "Bomfunk MC'S","Bruno Mars", "Bruno Mars", "Bruno Mars", "Justin Bieber", "Justin Bieber", "Justin Bieber", "Justin Bieber","Luis Fonsi","Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk", "Krzysztof Krawczyk","Sanah", "Sanah", "Sanah", "Sanah", "Sanah", "Sanah","Black Eyed Peas", "Black Eyed Peas", "Black Eyed Peas", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Maroon 5", "Beyoncé", "Beyoncé", "Justin Timberlake", "Justin Timberlake", "Nelly Furtado", "Nelly Furtado", "Nelly Furtado", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Rihanna", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Katy Perry", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Britney Spears", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "Lady Gaga", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "ABBA", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Michael Jackson", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Ed Sheeran", "Shawn Mendes", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Kwiat Jabłoni", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira", "Shakira" };
        List<string> songsTabPopRestart = new List<string>    { "Dla mnie masz stajla", "Jak Zapomnieć", "Becouse Of You", "Bo tutaj jest jak jest", "Do Nieba", "Żałuje", "Testosteron", "Asereje", "Powiedz", "Zawsze z Tobą chciałbym być", "W Kinie w Lublinie", "Sexbomb", "It's Raining Men", "Waiting for the night", "Let's Get Loud", "Freestyler", "When I was your Man", "Uptown Funk", "Lazy Song", "Stay", "Love Yourself", "Baby", "Sorry", "Despacito", "Bo jesteś Ty", "Chciałem być", "Za Tobą pójdę jak na bal", "Parostatek", "Zatańczysz ze mną jeszcze raz", "Ale jazz!", "Szampan", "No sory", "ten Stan", "Melodia", "2:00", "GIRL LIKE ME", "Pump It", "I Gotta Feeling", "Payphone ", "Animals", "Sugar", "One More Night", "Moves Like Jagger", "Single Ladies", "Halo", "Mirrors", "Can't Stop The Feeling", "Say It Right", "Maneater ", "Promiscuous", "Umbrella", "Only Girl (In the World)", "Disturbia", "Don't Stop the Music", "Diamonds", "Love The Way You Lie", "Roar", "E.T.", "Last Friday Night (T.G.I.F.)", "I Kissed a Girl", "Hot n Cold", "California Gurls", "Dark Horse", "Firework", "Hit Me Baby One More Time", "Oops!...I Did It Again", "I Wanna Go", "Toxic", "Womanizer", "Bad Romance", "Poker Face", "Paparazzi", "Applause", "Alejandro", "Slipping Through My Fingers", "Lay All Your Love On Me", "Waterloo", "Mamma Mia", "Dancing Queen", "Gimmie! Gimmie! Gimmie!", "Money, Money, Money", "Billie Jean", "Beat It", "Smooth Criminal", "Thriller", "Black Or White", "They Don't Care About Us", "Galway Girl", "I See Fire", "Bad Habits", "Perfect", "Shape Of You", "Stiches", "Idzie Zima", "Niemożliwe", "Dziś późno pójdę spać", "Wzięli zamknęli mi klub", "Loca", "Waka waka", "Rabiosa", "Can't remember to forget you", "Hips Don't Lie", "She Wolf", "Whenever, Wherever" };

        public MainPage()
        {
            
            InitializeComponent();
          
        }
        private void WTSGame()
        {
            //StreamReader srAllSongs = new StreamReader("AllSongs.txt");
            //StreamReader srPopSongs = new StreamReader("PopSongs.txt");
            //StreamReader srRockSongs = new StreamReader("RockSongs.txt");
            //StreamReader srFairyTalesSongs = new StreamReader("FairyTalesSongs.txt");
            //iAllSongs = new StreamReader("AllSongs.txt").ReadToEnd().Split(new char[] { '\n' }).Length;
            //iPopSongs = new StreamReader("PopSongs.txt").ReadToEnd().Split(new char[] { '\n' }).Length;
            //iRockSongs = new StreamReader("RockSongs.txt").ReadToEnd().Split(new char[] { '\n' }).Length;
            //iFairyTalesSongs = new StreamReader("FairyTalesSongs.txt").ReadToEnd().Split(new char[] { '\n' }).Length;

            while (newGame == true)
            {
                NavigationPage.SetHasNavigationBar(this, false);

                //if (firstImplementSongs = false)
                //{
                //    for (int i = 0; i < iAllSongs; i++)
                //    {
                //        authorsTab = 
                //    }
                //}
                Random r = new Random();
                


                Device.BeginInvokeOnMainThread(() =>
                {
                    seconds--;


                    if (authorsTab.Count < 1)
                    {
                        authorsTab = authorsTabRestart;
                        songsTab = songsTabRestart;
                    }
                    if (authorsTabFairyTales.Count < 1)
                    {
                        authorsTabFairyTales = authorsTabFairyTalesRestart;
                        songsTabFairyTales = songsTabFairyTalesRestart;
                    }
                    if (authorsTabRock.Count < 1)
                    {
                        authorsTabRock = authorsTabRockRestart;
                        songsTabRock = songsTabRockRestart;
                    }
                    if (authorsTabPop.Count < 1)
                    {
                        authorsTabPop = authorsTabPopRestart;
                        songsTabPop = songsTabPopRestart;
                    }
                    if (authorsTabRap.Count < 1)
                    {
                        authorsTabRap = authorsTabRapRestart;
                        songsTabRap = songsTabRapRestart;
                    }
                    if (timeLengthChanger == 1)
                    {
                        if (seconds > 16)
                        {
                            SongTitle.Text = (seconds - 16).ToString();
                            Time.IsVisible = false;
                            SongAuthor.IsVisible = false;
                            WrongAnswearButton.IsEnabled = false;
                            GoodAnswearButton.IsEnabled = false;
                            Rules1.IsVisible = true;
                            Rules2.IsVisible = true;
                            Rules3.IsVisible = true;
                        }
                        if (seconds < 16)
                        {
                            Time.Text = seconds.ToString();
                            Time.IsVisible = true;
                            SongAuthor.IsVisible = true;
                            WrongAnswearButton.IsEnabled = true;
                            GoodAnswearButton.IsEnabled = true;
                            Rules1.IsVisible = false;
                            Rules2.IsVisible = false;
                            Rules3.IsVisible = false;
                        }

                        if (seconds < 1)
                        {


                            BackgroundImage = "red.jpg";
                            lvlCounter++;
                            seconds = secondsValue;
                            Time.IsVisible = false;
                            WrongAnswearButton.IsEnabled = false;
                            GoodAnswearButton.IsEnabled = false;
                            if (typeOfSongsChanger == 1)
                            {
                                authorsTab.RemoveAt(songId);
                                songsTab.RemoveAt(songId);

                            }
                            if (typeOfSongsChanger == 2)
                            {
                                authorsTabFairyTales.RemoveAt(songFairyTaleId);
                                songsTabFairyTales.RemoveAt(songFairyTaleId);
                            }
                            if (typeOfSongsChanger == 3)
                            {
                                authorsTabRock.RemoveAt(songRockId);
                                songsTabRock.RemoveAt(songRockId);
                            }
                            if (typeOfSongsChanger == 4)
                            {
                                authorsTabPop.RemoveAt(songPopId);
                                songsTabPop.RemoveAt(songPopId);
                            }
                            if (typeOfSongsChanger == 5)
                            {
                                authorsTabRap.RemoveAt(songRapId);
                                songsTabRap.RemoveAt(songRapId);
                            }

                        }
                        if (seconds == 15)
                        {

                            songId = r.Next(authorsTab.Count);
                            songFairyTaleId = r.Next(authorsTabFairyTales.Count);
                            songRockId = r.Next(authorsTabRock.Count);
                            songPopId = r.Next(authorsTabPop.Count);
                            songRapId = r.Next(authorsTabRap.Count);
                            BackgroundImage = "blue.jpg";
                            if (typeOfSongsChanger == 1)
                            {
                               
                                SongAuthor.Text = authorsTab[songId];
                                SongTitle.Text = songsTab[songId];
                                
                               
                            }
                            if (typeOfSongsChanger == 2)
                            {
                                SongAuthor.Text = authorsTabFairyTales[songFairyTaleId];
                                SongTitle.Text = songsTabFairyTales[songFairyTaleId];
                            }
                            if (typeOfSongsChanger == 3)
                            {
                                SongAuthor.Text = authorsTabRock[songRockId];
                                SongTitle.Text = songsTabRock[songRockId];
                            }
                            if (typeOfSongsChanger == 4)
                            {
                                SongAuthor.Text = authorsTabPop[songPopId];
                                SongTitle.Text = songsTabPop[songPopId];
                            }
                            if (typeOfSongsChanger == 5)
                            {
                                SongAuthor.Text = authorsTabRap[songRapId];
                                SongTitle.Text = songsTabRap[songRapId];
                            }



                        }
                       
                        
                       
                    }

                    if (timeLengthChanger == 4)
                    {
                        if (seconds > 61)
                        {
                            SongTitle.Text = (seconds - 61).ToString();
                            Time.IsVisible = false;
                            SongAuthor.IsVisible = false;
                            WrongAnswearButton.IsEnabled = false;
                            GoodAnswearButton.IsEnabled = false;
                            Rules1.IsVisible = true;
                            Rules2.IsVisible = true;
                            Rules3.IsVisible = true;
                        }
                        if (seconds < 61)
                        {
                            Time.Text = seconds.ToString();
                            Time.IsVisible = true;
                            SongAuthor.IsVisible = true;
                            WrongAnswearButton.IsEnabled = true;
                            GoodAnswearButton.IsEnabled = true;
                            Rules1.IsVisible = false;
                            Rules2.IsVisible = false;
                            Rules3.IsVisible = false;
                        }

                        if (seconds < 1)
                        {


                            BackgroundImage = "red.jpg";
                            lvlCounter++;
                            seconds = secondsValue;
                            Time.IsVisible = false;
                            WrongAnswearButton.IsEnabled = false;
                            GoodAnswearButton.IsEnabled = false;
                            if (typeOfSongsChanger == 1)
                            {
                                authorsTab.RemoveAt(songId);
                                songsTab.RemoveAt(songId);

                            }
                            if (typeOfSongsChanger == 2)
                            {
                                authorsTabFairyTales.RemoveAt(songFairyTaleId);
                                songsTabFairyTales.RemoveAt(songFairyTaleId);
                            }
                            if (typeOfSongsChanger == 3)
                            {
                                authorsTabRock.RemoveAt(songRockId);
                                songsTabRock.RemoveAt(songRockId);
                            }
                            if (typeOfSongsChanger == 4)
                            {
                                authorsTabPop.RemoveAt(songPopId);
                                songsTabPop.RemoveAt(songPopId);
                            }
                            if (typeOfSongsChanger == 5)
                            {
                                authorsTabRap.RemoveAt(songRapId);
                                songsTabRap.RemoveAt(songRapId);
                            }
                        }
                        if (seconds == 60)
                        {

                            songId = r.Next(authorsTab.Count);
                            songFairyTaleId = r.Next(authorsTabFairyTales.Count);
                            songRockId = r.Next(authorsTabRock.Count);
                            songPopId = r.Next(authorsTabPop.Count);
                            songRapId = r.Next(authorsTabRap.Count);
                            BackgroundImage = "blue.jpg";
                            if (typeOfSongsChanger == 1)
                            {
                                SongAuthor.Text = authorsTab[songId];
                                SongTitle.Text = songsTab[songId];
                            }
                            if (typeOfSongsChanger == 2)
                            {
                                SongAuthor.Text = authorsTabFairyTales[songFairyTaleId];
                                SongTitle.Text = songsTabFairyTales[songFairyTaleId];
                            }
                            if (typeOfSongsChanger == 3)
                            {
                                SongAuthor.Text = authorsTabRock[songRockId];
                                SongTitle.Text = songsTabRock[songRockId];
                            }
                            if (typeOfSongsChanger == 4)
                            {
                                SongAuthor.Text = authorsTabPop[songPopId];
                                SongTitle.Text = songsTabPop[songPopId];
                            }
                            if (typeOfSongsChanger == 5)
                            {
                                SongAuthor.Text = authorsTabRap[songRapId];
                                SongTitle.Text = songsTabRap[songRapId];
                            }


                        }
                    }

                    if (timeLengthChanger == 3)
                    {
                        if (seconds > 46)
                        {
                            SongTitle.Text = (seconds - 46).ToString();
                            Time.IsVisible = false;
                            SongAuthor.IsVisible = false;
                            WrongAnswearButton.IsEnabled = false;
                            GoodAnswearButton.IsEnabled = false;
                            Rules1.IsVisible = true;
                            Rules2.IsVisible = true;
                            Rules3.IsVisible = true;
                        }
                        if (seconds < 46)
                        {
                            Time.Text = seconds.ToString();
                            Time.IsVisible = true;
                            SongAuthor.IsVisible = true;
                            WrongAnswearButton.IsEnabled = true;
                            GoodAnswearButton.IsEnabled = true;
                            Rules1.IsVisible = false;
                            Rules2.IsVisible = false;
                            Rules3.IsVisible = false;
                        }

                        if (seconds < 1)
                        {


                            BackgroundImage = "red.jpg";
                            lvlCounter++;
                            seconds = secondsValue;
                            Time.IsVisible = false;
                            WrongAnswearButton.IsEnabled = false;
                            GoodAnswearButton.IsEnabled = false;
                            if (typeOfSongsChanger == 1)
                            {
                                authorsTab.RemoveAt(songId);
                                songsTab.RemoveAt(songId);

                            }
                            if (typeOfSongsChanger == 2)
                            {
                                authorsTabFairyTales.RemoveAt(songFairyTaleId);
                                songsTabFairyTales.RemoveAt(songFairyTaleId);
                            }
                            if (typeOfSongsChanger == 3)
                            {
                                authorsTabRock.RemoveAt(songRockId);
                                songsTabRock.RemoveAt(songRockId);
                            }
                            if (typeOfSongsChanger == 4)
                            {
                                authorsTabPop.RemoveAt(songPopId);
                                songsTabPop.RemoveAt(songPopId);
                            }
                            if (typeOfSongsChanger == 5)
                            {
                                authorsTabRap.RemoveAt(songRapId);
                                songsTabRap.RemoveAt(songRapId);
                            }
                        }
                        if (seconds == 45)
                        {

                            songId = r.Next(authorsTab.Count);
                            songFairyTaleId = r.Next(authorsTabFairyTales.Count);
                            songRockId = r.Next(authorsTabRock.Count);
                            songPopId = r.Next(authorsTabPop.Count);
                            songRapId = r.Next(authorsTabRap.Count);
                            BackgroundImage = "blue.jpg";
                            if (typeOfSongsChanger == 1)
                            {
                                SongAuthor.Text = authorsTab[songId];
                                SongTitle.Text = songsTab[songId];
                            }
                            if (typeOfSongsChanger == 2)
                            {
                                SongAuthor.Text = authorsTabFairyTales[songFairyTaleId];
                                SongTitle.Text = songsTabFairyTales[songFairyTaleId];
                            }
                            if (typeOfSongsChanger == 3)
                            {
                                SongAuthor.Text = authorsTabRock[songRockId];
                                SongTitle.Text = songsTabRock[songRockId];
                            }
                            if (typeOfSongsChanger == 4)
                            {
                                SongAuthor.Text = authorsTabPop[songPopId];
                                SongTitle.Text = songsTabPop[songPopId];
                            }
                            if (typeOfSongsChanger == 5)
                            {
                                SongAuthor.Text = authorsTabRap[songRapId];
                                SongTitle.Text = songsTabRap[songRapId];
                            }


                        }
                    }

                    if (timeLengthChanger == 2)
                    {
                        if (seconds > 31)
                        {
                            SongTitle.Text = (seconds - 31).ToString();
                            Time.IsVisible = false;
                            SongAuthor.IsVisible = false;
                            WrongAnswearButton.IsEnabled = false;
                            GoodAnswearButton.IsEnabled = false;
                            Rules1.IsVisible = true;
                            Rules2.IsVisible = true;
                            Rules3.IsVisible = true;
                        }
                        if (seconds < 31)
                        {
                            Time.Text = seconds.ToString();
                            Time.IsVisible = true;
                            SongAuthor.IsVisible = true;
                            WrongAnswearButton.IsEnabled = true;
                            GoodAnswearButton.IsEnabled = true;
                            Rules1.IsVisible = false;
                            Rules2.IsVisible = false;
                            Rules3.IsVisible = false;
                        }

                        if (seconds < 1)
                        {


                            BackgroundImage = "red.jpg";
                            lvlCounter++;
                            seconds = secondsValue;
                            Time.IsVisible = false;
                            WrongAnswearButton.IsEnabled = false;
                            GoodAnswearButton.IsEnabled = false;
                            if (typeOfSongsChanger == 1)
                            {
                                authorsTab.RemoveAt(songId);
                                songsTab.RemoveAt(songId);

                            }
                            if (typeOfSongsChanger == 2)
                            {
                                authorsTabFairyTales.RemoveAt(songFairyTaleId);
                                songsTabFairyTales.RemoveAt(songFairyTaleId);
                            }
                            if (typeOfSongsChanger == 3)
                            {
                                authorsTabRock.RemoveAt(songRockId);
                                songsTabRock.RemoveAt(songRockId);
                            }
                            if (typeOfSongsChanger == 4)
                            {
                                authorsTabPop.RemoveAt(songPopId);
                                songsTabPop.RemoveAt(songPopId);
                            }
                            if (typeOfSongsChanger == 5)
                            {
                                authorsTabRap.RemoveAt(songRapId);
                                songsTabRap.RemoveAt(songRapId);
                            }
                        }
                        if (seconds == 30)
                        {

                            songId = r.Next(authorsTab.Count);
                            songFairyTaleId = r.Next(authorsTabFairyTales.Count);
                            songRockId = r.Next(authorsTabRock.Count);
                            songPopId = r.Next(authorsTabPop.Count);
                            songRapId = r.Next(authorsTabRap.Count);
                            BackgroundImage = "blue.jpg";
                            if (typeOfSongsChanger == 1)
                            {
                                SongAuthor.Text = authorsTab[songId];
                                SongTitle.Text = songsTab[songId];
                            }
                            if (typeOfSongsChanger == 2)
                            {
                                SongAuthor.Text = authorsTabFairyTales[songFairyTaleId];
                                SongTitle.Text = songsTabFairyTales[songFairyTaleId];
                            }
                            if (typeOfSongsChanger == 3)
                            {
                                SongAuthor.Text = authorsTabRock[songRockId];
                                SongTitle.Text = songsTabRock[songRockId];
                            }
                            if (typeOfSongsChanger == 4)
                            {
                                SongAuthor.Text = authorsTabPop[songPopId];
                                SongTitle.Text = songsTabPop[songPopId];
                            }
                            if (typeOfSongsChanger == 5)
                            {
                                SongAuthor.Text = authorsTabRap[songRapId];
                                SongTitle.Text = songsTabRap[songRapId];
                            }


                        }
                    }
                    if (lvlCounter==10)
                    {
                        
                        BackgroundImage = "kosmos.jpg";
                        
                        SongTitle.IsVisible = true;
                        Time.IsVisible = false;
                        SongAuthor.IsVisible = false;
                        WrongAnswearButton.IsVisible = false;
                        GoodAnswearButton.IsVisible = false;
                        WrongAnswearButton.IsEnabled = false;
                        GoodAnswearButton.IsEnabled = false;
                        SongTitle.Text = "Prawidłowe odpowiedzi: " + pointsCounter.ToString() + "/10";
                        newGame = false;
                        BackButton.IsEnabled = true;
                        BackButton.IsVisible = true;
                        
                        BackButtonImage.IsVisible = true;
                        OnceAgain.IsEnabled = true;
                        OnceAgain.IsVisible = true;
                        OnceAgainImage.IsVisible = true;

                    }
                    
                    
                });
                Thread.Sleep(1000);
            }





        }
        
               
                   

            
        


        private void WrongAnswearButton_Clicked(object sender, EventArgs e)
        {

            BackgroundImage = "red.jpg";
            seconds = secondsValue;
            Time.IsVisible = false;
            WrongAnswearButton.IsEnabled = false;
            GoodAnswearButton.IsEnabled = false;
            lvlCounter++;

            if (typeOfSongsChanger == 1)
            {
                authorsTab.RemoveAt(songId);
                songsTab.RemoveAt(songId);

            }
            if (typeOfSongsChanger == 2)
            {
                authorsTabFairyTales.RemoveAt(songFairyTaleId);
                songsTabFairyTales.RemoveAt(songFairyTaleId);
            }
            if (typeOfSongsChanger == 3)
            {
                authorsTabRock.RemoveAt(songRockId);
                songsTabRock.RemoveAt(songRockId);
            }
            if (typeOfSongsChanger == 4)
            {
                authorsTabPop.RemoveAt(songPopId);
                songsTabPop.RemoveAt(songPopId);
            }
            if (typeOfSongsChanger == 5)
            {
                authorsTabRap.RemoveAt(songRapId);
                songsTabRap.RemoveAt(songRapId);
            }
        }

        private void GoodAnswearButton_Clicked(object sender, EventArgs e)
        {

            BackgroundImage = "green.jpg";
            seconds = secondsValue;
            Time.IsVisible = false;
            WrongAnswearButton.IsEnabled = false;
            GoodAnswearButton.IsEnabled = false;
            lvlCounter++;
            if (typeOfSongsChanger == 1)
            {
                authorsTab.RemoveAt(songId);
                songsTab.RemoveAt(songId);

            }
            if (typeOfSongsChanger == 2)
            {
                authorsTabFairyTales.RemoveAt(songFairyTaleId);
                songsTabFairyTales.RemoveAt(songFairyTaleId);
            }
            if (typeOfSongsChanger == 3)
            {
                authorsTabRock.RemoveAt(songRockId);
                songsTabRock.RemoveAt(songRockId);
            }
            if (typeOfSongsChanger == 4)
            {
                authorsTabPop.RemoveAt(songPopId);
                songsTabPop.RemoveAt(songPopId);
            }
            if (typeOfSongsChanger == 5)
            {
                authorsTabRap.RemoveAt(songRapId);
                songsTabRap.RemoveAt(songRapId);
            }
            pointsCounter++;
        }

        private void NewGame_Clicked(object sender, EventArgs e)
        {
            NewGameAppear();
           
            if (timeLengthChanger == 1)
            {
                secondsValue = 17;
                seconds = 20;
            }
            if (timeLengthChanger == 2)
            {
                secondsValue = 32;
                seconds = 35;
            }
            if (timeLengthChanger == 3)
            {
                secondsValue = 47;
                seconds = 50;
            }
            if (timeLengthChanger == 4)
            {
                secondsValue = 62;
                seconds = 65;
            }

            Task modifyTaskOne = Task.Run(() => WTSGame());
           
            //TimeCounter();
        }

       

        private void BackButton_Clicked(object sender, EventArgs e)
        {
           
            if (backButtonChanger == 1)
            {
                NewGameDisappear();
                SongAuthor.Text = "";
                SongTitle.Text = "";
                OnceAgain.IsVisible = false;
                OnceAgain.IsEnabled = false;
                Settings.IsEnabled = true;
                Settings.IsVisible = true;
            }
            if (backButtonChanger == 2)
            {
               
                MenuAppear();
                BackButton.IsEnabled = false;
                BackButton.IsVisible = false;
                
                BackButtonImage.IsVisible = false;
                TimeSettingsImage.IsVisible = false;
                

            }
            if (backButtonChanger == 3)
            {
                Time15.IsEnabled = false;
                Time15.IsVisible = false;
                Time30.IsEnabled = false;
                Time30.IsVisible = false;
                Time45.IsEnabled = false;
                Time45.IsVisible = false;
                Time60.IsEnabled = false;
                Time60.IsVisible = false;
             
                Time15Image.IsVisible = false;
               
                Time30Image.IsVisible = false;
                
                Time45Image.IsVisible = false;
            
                Time60Image.IsVisible = false;
                AllSongs.IsEnabled = false;
                AllSongs.IsVisible = false;
                FairyTaleSongs.IsEnabled = false;
                FairyTaleSongs.IsVisible = false;
                RockSongs.IsEnabled = false;
                RockSongs.IsVisible = false;
                PopSongs.IsEnabled = false;
                PopSongs.IsVisible = false;
                RapSongs.IsEnabled = false;
                RapSongs.IsVisible = false;
                rapImage.IsVisible = false;
                AllSongsImage.IsVisible = false;
                FairyTaleSongsImage.IsVisible = false;
                RockSongsImage.IsVisible = false;
                PopSongsImage.IsVisible = false;

                MenuAppear2();
                backButtonChanger = 2;
            }
            
        }
        private void NewGameAppear()
        {
            Settings.IsEnabled = false;
            Settings.IsVisible = false;
            NewGame.IsEnabled = false;
            NewGame.IsVisible = false;
            NewGameImage.IsVisible = false;
            SettingsImage.IsVisible = false;
            GoodAnswearButton.IsEnabled = true;
            GoodAnswearButton.IsVisible = true;
            WrongAnswearButton.IsEnabled = true;
            WrongAnswearButton.IsVisible = true;
            SongAuthor.IsEnabled = true;
            SongAuthor.IsVisible = true;
            SongTitle.IsEnabled = true;
            SongTitle.IsVisible = true;
            Time.IsVisible = true;
            OnceAgain.IsVisible = false;
            OnceAgain.IsEnabled = false;
            OnceAgainImage.IsVisible = false;
            BackButton.IsVisible = false;
            BackButton.IsEnabled = false;
            BackButtonImage.IsVisible = false;
            
            //Border.IsVisible = false;
            //Border.IsEnabled = false;
            newGame = true;
           
            
            lvlCounter = 0;
            pointsCounter = 0;
            TitleOfGame.IsVisible = false;
            AFTM.IsVisible = false;
            backButtonChanger = 1;

        }
        private void NewGameDisappear()
        {
            NewGame.IsEnabled = true;
            NewGame.IsVisible = true;
            NewGameImage.IsVisible = true;
            SettingsImage.IsVisible = true;
            GoodAnswearButton.IsEnabled = false;
            GoodAnswearButton.IsVisible = false;
            WrongAnswearButton.IsEnabled = false;
            WrongAnswearButton.IsVisible = false;
            SongAuthor.IsEnabled = false;
            SongAuthor.IsVisible = false;
            SongTitle.IsEnabled = false;
            SongTitle.IsVisible = false;
            Time.IsVisible = false;
            BackButton.IsVisible = false;
            BackButton.IsEnabled = false;
            OnceAgainImage.IsVisible = false;
            BackButtonImage.IsVisible = false;
            
            newGame = false;
            TitleOfGame.IsVisible = true;
            AFTM.IsVisible = true;
            //Border.IsVisible = true;
            //Border.IsEnabled = true;

        }

        private void Settings_Clicked(object sender, EventArgs e)
        {
            MenuDisappear();
            BackButton.IsEnabled = true;
            BackButton.IsVisible = true;
            BackButtonImage.IsVisible = true;

            TimeSettingsImage.IsVisible = true;
            TypeOfGameImage.IsVisible = true;

            TimeSettings.IsEnabled = true;
            TimeSettings.IsVisible = true;
            TypeOfGame.IsEnabled = true;
            TypeOfGame.IsVisible = true;
            backButtonChanger = 2;
            TitleOfGame.IsEnabled = false;
            TitleOfGame.IsVisible = false;
            AFTM.IsEnabled = false;
            AFTM.IsVisible = false;
        }

        private void TypeOfGame_Clicked(object sender, EventArgs e)
        {
            MenuDisappear2();
            AllSongs.IsEnabled = true;
            AllSongs.IsVisible = true;
            FairyTaleSongs.IsEnabled = true;
            FairyTaleSongs.IsVisible = true;
            RockSongs.IsEnabled = true;
            RockSongs.IsVisible = true;
            PopSongs.IsEnabled = true;
            PopSongs.IsVisible = true;
            RapSongs.IsEnabled = true;
            RapSongs.IsVisible = true;
            rapImage.IsVisible = true;
            AllSongsImage.IsVisible = true;
            FairyTaleSongsImage.IsVisible = true;
            RockSongsImage.IsVisible = true;
            PopSongsImage.IsVisible = true;
            backButtonChanger = 3;
        }

        private void TimeSettings_Clicked(object sender, EventArgs e)
        {
            MenuDisappear2();
            backButtonChanger = 3;
            Time15.IsEnabled = true;
            Time15.IsVisible = true;
            Time30.IsEnabled = true;
            Time30.IsVisible = true;
            Time45.IsEnabled = true;
            Time45.IsVisible = true;
            Time60.IsEnabled = true;
            Time60.IsVisible = true;
            TimeSettingsImage.IsVisible = false;
            
            Time15Image.IsVisible = true;
            
            Time30Image.IsVisible = true;
            
            Time45Image.IsVisible = true;
            
            Time60Image.IsVisible = true;

        }

        private void Time15_Clicked(object sender, EventArgs e)
        {
            timeLengthChanger = 1;
            Time15.BorderColor = Color.Red;
            Time30.BorderColor = Color.AliceBlue;
            Time45.BorderColor = Color.AliceBlue;
            Time60.BorderColor = Color.AliceBlue;
            seconds = 20;
            secondsValue = 17;

        }

        private void Time30_Clicked(object sender, EventArgs e)
        {
            timeLengthChanger = 2;
            Time15.BorderColor = Color.AliceBlue;
            Time30.BorderColor = Color.Red;
            Time45.BorderColor = Color.AliceBlue;
            Time60.BorderColor = Color.AliceBlue;
            seconds = 35;
            secondsValue = 32;
        }

        private void Time45_Clicked(object sender, EventArgs e)
        {
            timeLengthChanger = 3;
            Time15.BorderColor = Color.AliceBlue;
            Time30.BorderColor = Color.AliceBlue;
            Time45.BorderColor = Color.Red;
            Time60.BorderColor = Color.AliceBlue;
            seconds = 50;
            secondsValue = 47;
        }

        private void Time60_Clicked(object sender, EventArgs e)
        {
            timeLengthChanger = 4;
            Time15.BorderColor = Color.AliceBlue;
            Time30.BorderColor = Color.AliceBlue;
            Time45.BorderColor = Color.AliceBlue;
            Time60.BorderColor = Color.Red;
            seconds = 65;
            secondsValue = 62;
        }

        private void AllSongs_Clicked(object sender, EventArgs e)
        {
            typeOfSongsChanger = 1;
            AllSongs.BorderColor = Color.Red;
            FairyTaleSongs.BorderColor = Color.AliceBlue;
            RockSongs.BorderColor = Color.AliceBlue;
            PopSongs.BorderColor = Color.AliceBlue;
            RapSongs.BorderColor = Color.AliceBlue;
           
        }

        private void FairyTaleSongs_Clicked(object sender, EventArgs e)
        {
            typeOfSongsChanger = 2;
            AllSongs.BorderColor = Color.AliceBlue;
            FairyTaleSongs.BorderColor = Color.Red;
            RockSongs.BorderColor = Color.AliceBlue;
            PopSongs.BorderColor = Color.AliceBlue;
            RapSongs.BorderColor = Color.AliceBlue;
           
        }

        private void RockSongs_Clicked(object sender, EventArgs e)
        {
            typeOfSongsChanger = 3;
            AllSongs.BorderColor = Color.AliceBlue;
            FairyTaleSongs.BorderColor = Color.AliceBlue;
            RockSongs.BorderColor = Color.Red;
            PopSongs.BorderColor = Color.AliceBlue;
            RapSongs.BorderColor = Color.AliceBlue;
           
        }

        private void PopSongs_Clicked(object sender, EventArgs e)
        {
            typeOfSongsChanger = 4;
            AllSongs.BorderColor = Color.AliceBlue;
            FairyTaleSongs.BorderColor = Color.AliceBlue;
            RockSongs.BorderColor = Color.AliceBlue;
            PopSongs.BorderColor = Color.Red;
            RapSongs.BorderColor = Color.AliceBlue;
            
        }
        private void RapSongs_Clicked(object sender, EventArgs e)
        {
            typeOfSongsChanger = 5;
            AllSongs.BorderColor = Color.AliceBlue;
            FairyTaleSongs.BorderColor = Color.AliceBlue;
            RockSongs.BorderColor = Color.AliceBlue;
            PopSongs.BorderColor = Color.AliceBlue;
            RapSongs.BorderColor = Color.Red;
           
        }
        private void MenuDisappear()
        {
            TimeSettings.IsEnabled = true;
            TimeSettings.IsVisible = true;
            TypeOfGame.IsEnabled = true;
            TypeOfGame.IsVisible = true;
            TypeOfGameImage.IsVisible = true;
            NewGame.IsEnabled = false;
            NewGame.IsVisible = false;
            NewGameImage.IsVisible = false;
            SettingsImage.IsVisible = false;
            Settings.IsEnabled = false;
            Settings.IsVisible = false;

        }
        private void MenuAppear()
        {
            TitleOfGame.IsVisible = true;
            AFTM.IsVisible = true;
            TimeSettings.IsEnabled = false;
            TimeSettings.IsVisible = false;
            TypeOfGame.IsEnabled = false;
            TypeOfGame.IsVisible = false;
            TypeOfGameImage.IsVisible = false;
            NewGame.IsEnabled = true;
            NewGame.IsVisible = true;
            NewGameImage.IsVisible = true;
            SettingsImage.IsVisible = true;
            Settings.IsEnabled = true;
            Settings.IsVisible = true;

        }
        private void MenuDisappear2()
        {
            TimeSettings.IsEnabled = false;
            TimeSettings.IsVisible = false;
            TypeOfGame.IsEnabled = false;
            TypeOfGame.IsVisible = false;
            TypeOfGameImage.IsVisible = false;
            TimeSettingsImage.IsVisible = false;

        }
        private void MenuAppear2()
        {
            TimeSettings.IsEnabled = true;
            TimeSettings.IsVisible = true;
            TypeOfGame.IsEnabled = true;
            TypeOfGame.IsVisible = true;
            TypeOfGameImage.IsVisible = true;
            TimeSettingsImage.IsVisible = true;

        }

       


        //public ListaPiosenek(string nazwaPiosenki, string autorPiosenki)
        //{
        //    return (nazwaPiosenki, autorPiosenki);
        //}

    }
}
