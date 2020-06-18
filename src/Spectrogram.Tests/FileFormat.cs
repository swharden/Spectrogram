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
            (int sampleRate, double[] audio) = WavFile.ReadMono("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 1 << 12;
            var spec = new Spectrogram(sampleRate, fftSize, stepSize: 700, maxFreq: 2000);
            spec.SetWindow(FftSharp.Window.Hanning(fftSize / 3)); // sharper window than typical
            spec.Add(audio);
            spec.SaveData("hal.sff");

            var spec2 = new SFF("hal.sff");
            Assert.AreEqual(spec.SampleRate, spec2.SampleRate);
            Assert.AreEqual(spec.StepSize, spec2.StepSize);
            Assert.AreEqual(spec.Width, spec2.Width);
            Assert.AreEqual(spec.FftSize, spec2.FftSize);
            Assert.AreEqual(spec.NextColumnIndex, spec2.FftFirstIndex);
            Assert.AreEqual(spec.Height, spec2.Height);
            Assert.AreEqual(spec.OffsetHz, spec2.OffsetHz);
        }
    }
}
