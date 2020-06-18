using NUnit.Framework;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Spectrogram.Tests
{
    public class Quickstart
    {
        [Test]
        public void Test_Quickstart_Hal()
        {
            (int sampleRate, double[] audio) = WavFile.ReadMono("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 4096;
            var spec = new Spectrogram(sampleRate, fftSize, stepSize: 500, maxFreq: 3000);
            spec.Add(audio);
            spec.SaveImage("../../../../../dev/graphics/hal.png", intensity: .2);
            
            Console.WriteLine(spec);
        }

        [Test]
        public void Test_Quickstart_Handel()
        {
            double[] audio = Mp3.Read("../../../../../data/Handel - Air and Variations.mp3");
            int sampleRate = 44100;

            int fftSize = 16384;
            int targetWidthPx = 3000;
            int stepSize = audio.Length / targetWidthPx;

            var spec = new Spectrogram(sampleRate, fftSize, stepSize, maxFreq: 2200);
            spec.Add(audio);
            spec.SaveImage("../../../../../dev/spectrogram-song.jpg", intensity: 5, dB: true);

            Console.WriteLine(spec);
            /*
             Spectrogram (2993, 817)
               Vertical (817 px): 0 - 2,199 Hz, FFT size: 16,384 samples, 2.69 Hz/px
               Horizontal (2993 px): 2.96 min, window: 0.37 sec, step: 0.06 sec, overlap: 84%
            */
        }
    }
}