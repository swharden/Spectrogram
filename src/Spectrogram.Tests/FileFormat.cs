using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Spectrogram.Tests
{
    class FileFormat
    {
        [Test]
        public void Test_SFF_Linear()
        {
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 1 << 12;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 700, maxFreq: 2000);
            spec.SetWindow(FftSharp.Window.Hanning(fftSize / 3)); // sharper window than typical
            spec.Add(audio);
            spec.SaveData("../../../../../dev/sff/hal.sff");

            var spec2 = new SFF("../../../../../dev/sff/hal.sff");
            Assert.AreEqual(spec.SampleRate, spec2.SampleRate);
            Assert.AreEqual(spec.StepSize, spec2.StepSize);
            Assert.AreEqual(spec.Width, spec2.Width);
            Assert.AreEqual(spec.FftSize, spec2.FftSize);
            Assert.AreEqual(spec.NextColumnIndex, spec2.FftFirstIndex);
            Assert.AreEqual(spec.Height, spec2.Height);
            Assert.AreEqual(spec.OffsetHz, spec2.OffsetHz);
        }

        [Test]
        public void Test_SFF_Mel()
        {
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/cant-do-that-44100.wav");
            int fftSize = 1 << 12;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 700);
            spec.SetWindow(FftSharp.Window.Hanning(fftSize / 3)); // sharper window than typical
            spec.Add(audio);

            Bitmap bmp = spec.GetBitmapMel(250);
            bmp.Save("../../../../../dev/sff/halMel.png", System.Drawing.Imaging.ImageFormat.Png);
            spec.SaveData("../../../../../dev/sff/halMel.sff", melBinCount: 250);

            var spec2 = new SFF("../../../../../dev/sff/halMel.sff");
            Assert.AreEqual(spec.SampleRate, spec2.SampleRate);
            Assert.AreEqual(spec.StepSize, spec2.StepSize);
            Assert.AreEqual(spec.Width, spec2.Width);
            Assert.AreEqual(spec.FftSize, spec2.FftSize);
            Assert.AreEqual(spec.NextColumnIndex, spec2.FftFirstIndex);
            Assert.AreEqual(spec.Height, spec2.Height);
            Assert.AreEqual(spec.OffsetHz, spec2.OffsetHz);
        }

        [Test]
        public void Test_SFF_Linear2()
        {
            // test creating SFF file from 16-bit 48kHz mono WAV file

            // read the wav file
            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/03-02-03-01-02-01-19.wav");
            Assert.AreEqual(48000, sampleRate);

            // save the SFF
            int fftSize = 1 << 12;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 300, maxFreq: 2000);
            spec.Add(audio);
            spec.SaveData("testDoor.sff");

            // load the SFF and verify all the values are the same
            var spec2 = new SFF("testDoor.sff");
            Assert.AreEqual(spec.SampleRate, spec2.SampleRate);
            Assert.AreEqual(spec.StepSize, spec2.StepSize);
            Assert.AreEqual(spec.Width, spec2.Width);
            Assert.AreEqual(spec.FftSize, spec2.FftSize);
            Assert.AreEqual(spec.NextColumnIndex, spec2.FftFirstIndex);
            Assert.AreEqual(spec.Height, spec2.Height);
            Assert.AreEqual(spec.OffsetHz, spec2.OffsetHz);
            Assert.AreEqual("SFF 701x170", spec2.ToString());
        }

        [Test]
        public void Test_SFF_LinearBigMaxFreq()
        {
            // test creating SFF file from 16-bit 48kHz mono WAV file

            (double[] audio, int sampleRate) = AudioFile.ReadWAV("../../../../../data/03-02-03-01-02-01-19.wav");
            Assert.AreEqual(48000, sampleRate);

            int fftSize = 1 << 12;
            var spec = new SpectrogramGenerator(sampleRate, fftSize, stepSize: 300, maxFreq: 7999);
            spec.Add(audio);
            spec.SaveData("testDoorBig.sff");
        }
    }
}
