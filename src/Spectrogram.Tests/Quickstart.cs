using NUnit.Framework;
using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Spectrogram.Tests
{
    public class Tests
    {
        [Test]
        public void Test_WholeFile_DefaultSettings()
        {
            double[] audio = Read.WavInt16mono("../../../../../data/cant-do-that-44100.wav");

            var spec = new Spectrogram(sampleRate: 44100, fftSize: 1 << 12, stepSize: 500, maxFreq: 3000);
            spec.Add(audio);
            spec.SaveImage("../../../../../dev/graphics/hal.png", intensity: .2);
            // TODO: colormap
            Console.WriteLine(spec);
        }
    }
}