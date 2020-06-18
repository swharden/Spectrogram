using NUnit.Framework;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Tests
{
    class Wav
    {
        [Test]
        public void Test_WavFile_ReadHal1()
        {
            string wavFilePath = "../../../../../data/cant-do-that-44100.wav";
            (int sampleRate, double[] L) = WavFile.ReadMono(wavFilePath);
            Assert.AreEqual(44100, sampleRate);

            double lengthSec = (double)L.Length / sampleRate;
            Assert.AreEqual(3.779, lengthSec, .01);

            var plt = new Plot();
            plt.PlotSignal(L);
            plt.SaveFig("hal1.png");
        }

        [Test]
        public void Test_WavFile_ReadHal2()
        {
            string wavFilePath = "../../../../../data/cant-do-that-11025-stereo.wav";
            (int sampleRate, double[] L, double[] R) = WavFile.ReadStereo(wavFilePath);
            Assert.AreEqual(11025, sampleRate);

            double lengthSec = (double)L.Length / sampleRate;
            Assert.AreEqual(3.779, lengthSec, .01);

            var plt = new Plot();
            plt.PlotSignal(L);
            plt.PlotSignal(R);
            plt.SaveFig("hal2.png");
        }
    }
}
