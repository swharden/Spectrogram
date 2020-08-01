using NAudio.Wave;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrogram.Tests
{
    class WavUsingLibrary
    {
        [Test]
        public void Test_WavFile_ReadASehgal()
        {
            // READ THE WAV FILE WITH NAUDIO
            double[] audio;
            int sampleRate;
            string wavFilePath = "../../../../../data/asehgal-original.wav";

            using (var audioFileReader = new AudioFileReader(wavFilePath))
            {
                sampleRate = audioFileReader.WaveFormat.SampleRate;
                var wholeFile = new List<float>((int)(audioFileReader.Length / 4));
                var readBuffer = new float[audioFileReader.WaveFormat.SampleRate * audioFileReader.WaveFormat.Channels];
                int samplesRead = 0;
                while ((samplesRead = audioFileReader.Read(readBuffer, 0, readBuffer.Length)) > 0)
                    wholeFile.AddRange(readBuffer.Take(samplesRead));
                audio = Array.ConvertAll(wholeFile.ToArray(), x => (double)x);
            }

            // TEST VALUES ARE WHAT WE EXPECT
            double lengthSec = (double)audio.Length / sampleRate;
            Assert.AreEqual(40_000, sampleRate);
            Assert.AreEqual(40, lengthSec, .01);

            // CREATE THE SPECTROGRAM
            int fftSize = 8192;
            var spec = new Spectrogram(sampleRate, fftSize, stepSize: 4_000, maxFreq: 2_000);
            spec.Add(audio);
            spec.SaveImage("asehgal.png", intensity: 10_000, dB: true);
        }
    }
}
