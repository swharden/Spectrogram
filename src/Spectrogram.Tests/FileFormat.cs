using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Tests
{
    class FileFormat
    {
        [Test]
        public void Test_Save_Format()
        {
            (int sampleRate, double[] audio, _) = WavFile.Read("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 1 << 12;
            var spec = new Spectrogram(sampleRate, fftSize, stepSize: 700, maxFreq: 2000);
            spec.SetWindow(FftSharp.Window.Hanning(fftSize / 3)); // sharper window than typical
            spec.Add(audio);
            spec.SaveData("hal.sff");
        }
    }
}
