using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spectrogram.Tests
{
    public class Quickstart
    {
        [Test]
        public void Test_Quickstart_Hal()
        {
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav");
            var sg = new SpectrogramGenerator(sampleRate, fftSize: 4096, stepSize: 500, maxFreq: 3000);
            sg.Add(audio);
            sg.SaveImage("../../../../../dev/graphics/hal.png");
            
            Console.WriteLine(sg);
        }

        [Test]
        public void Test_Readme_HeaderImage()
        {
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 2048;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 400, maxFreq: 6000);
            spec.Add(audio);
            spec.SaveImage("../../../../../dev/graphics/hal-spectrogram.png", intensity: 10, dB: true, dBScale: .05);

            Console.WriteLine(spec);
        }

        [Test]
        public void Test_Quickstart_Handel()
        {
            double[] audio = AudioFile.ReadMP3("../../../../../data/Handel - Air and Variations.mp3");
            int sampleRate = 44100;

            int fftSize = 16384;
            int targetWidthPx = 3000;
            int stepSize = audio.Length / targetWidthPx;

            var sg = new SpectrogramGenerator(sampleRate, fftSize, stepSize, maxFreq: 2200);
            sg.Add(audio);
            sg.SaveImage("../../../../../dev/graphics/spectrogram-song.png", intensity: 5, dB: true);

            Console.WriteLine(sg);
            /*
             Spectrogram (2993, 817)
               Vertical (817 px): 0 - 2,199 Hz, FFT size: 16,384 samples, 2.69 Hz/px
               Horizontal (2993 px): 2.96 min, window: 0.37 sec, step: 0.06 sec, overlap: 84%
            */
        }
    }
}