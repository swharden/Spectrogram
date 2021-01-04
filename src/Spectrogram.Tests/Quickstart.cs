using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spectrogram.Tests
{
    public class Quickstart
    {
        private static (double[] audio, int sampleRate) ReadWavWithNAudio(string filePath)
        {
            using var afr = new NAudio.Wave.AudioFileReader(filePath);
            int sampleRate = afr.WaveFormat.SampleRate;
            int sampleCount = (int)(afr.Length / afr.WaveFormat.BitsPerSample / 8);
            int channelCount = afr.WaveFormat.Channels;
            var audio = new List<double>(sampleCount);
            var buffer = new float[sampleRate * channelCount];
            int samplesRead = 0;
            while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
                audio.AddRange(buffer.Take(samplesRead).Select(x => (double)x));
            return (audio.ToArray(), sampleRate);
        }

        [Test]
        public void Test_Quickstart_Hal()
        {
            (double[] audio, int sampleRate) = WavFile.ReadWavWithNAudio("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 4096;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 500, maxFreq: 3000);
            spec.Add(audio);
            spec.SaveImage("../../../../../dev/graphics/hal.png");
            
            Console.WriteLine(spec);
        }

        [Test]
        public void Test_Readme_HeaderImage()
        {
            (double[] audio, int sampleRate) = WavFile.ReadWavWithNAudio("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 2048;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 400, maxFreq: 6000);
            spec.Add(audio);
            spec.SaveImage("../../../../../dev/graphics/hal-spectrogram.png", intensity: 10, dB: true, dBScale: .05);

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

            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize, maxFreq: 2200);
            spec.Add(audio);
            spec.SaveImage("../../../../../dev/graphics/spectrogram-song.png", intensity: 5, dB: true);

            Console.WriteLine(spec);
            /*
             Spectrogram (2993, 817)
               Vertical (817 px): 0 - 2,199 Hz, FFT size: 16,384 samples, 2.69 Hz/px
               Horizontal (2993 px): 2.96 min, window: 0.37 sec, step: 0.06 sec, overlap: 84%
            */
        }
    }
}