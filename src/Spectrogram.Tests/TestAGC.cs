using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrogram.Tests
{
    class TestAGC
    {
        //[Test]
        public void Test_QRSS_noAGC()
        {
            string wavFilePath = "../../../../../data/qrss-10min.wav";
            (int sampleRate, double[] L) = WavFile.ReadMono(wavFilePath);
            

            int fftSize = 8192;
            var spec = new Spectrogram(sampleRate, fftSize, stepSize: 2000, maxFreq: 3000);
            spec.Add(L);
            spec.SaveImage("qrss-AGCoff.png", intensity: 3);

            var ffts = spec.GetFFTs();
            double normalIntensity = 2;
            for (int i = 0; i < ffts.Count; i++)
            {
                double[] sorted = new double[ffts[i].Length];
                ffts[i].CopyTo(sorted, 0);
                Array.Sort(sorted);

                double percentile = 0.25;
                int percentileIndex = (int)(percentile * ffts[0].Length);
                double floorValue = sorted[percentileIndex];

                for (int y = 0; y < ffts[i].Length; y++)
                {
                    ffts[i][y] = ffts[i][y] / floorValue * normalIntensity;
                }

                Console.WriteLine(floorValue);
            }

            spec.SaveImage("qrss-AGCon.png", intensity: 3);
        }
    }
}
