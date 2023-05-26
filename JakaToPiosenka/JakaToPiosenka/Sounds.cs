using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace JakaToPiosenka
{
    internal class Sounds
    {
        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("JakaToPiosenka.Sounds." + filename);

            return stream;
        }

        void PlaySound(Stream fileName)
        {
            var audio = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            audio.Load(fileName);
            audio.Play();
        }
        public void WinningSound()
        {
            Stream fileName = GetStreamFromFile("winSound.wav");
            PlaySound(fileName);
        }

        public void LosingSound()
        {
            Stream fileName = GetStreamFromFile("loseSound.wav");
            PlaySound(fileName);
        }

        public void ClickSound()
        {
            Stream fileName = GetStreamFromFile("clickSound.wav");
            PlaySound(fileName);
        }

        public void DeleteSound()
        {
            Stream fileName = GetStreamFromFile("deleteSound.wav");
            PlaySound(fileName);
        }

        public void CountdownSound()
        {
            Stream fileName = GetStreamFromFile("countdownSound.wav");
            PlaySound(fileName);
        }

        public void GoodScoreSound()
        {
            Stream fileName = GetStreamFromFile("goodScoreSound.wav");
            PlaySound(fileName);
        }

        public void BadScoreSound()
        {
            Stream fileName = GetStreamFromFile("badScoreSound.wav");
            PlaySound(fileName);
        }

        public void MediumScoreSound()
        {
            Stream fileName = GetStreamFromFile("mediumScoreSound.wav");
            PlaySound(fileName);
        }
    }
}
