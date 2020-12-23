using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrogram.Tests
{
    class TestAGC
    {
        [Test]
        public void Test_AGC_off()
        {
            string wavFilePath = "../../../../../data/qrss-10min.wav";
            (int sampleRate, double[] L) = WavFile.ReadMono(wavFilePath);

            int fftSize = 8192;
            var spec = new SGram(sampleRate, fftSize, stepSize: 2000, maxFreq: 3000);
            spec.Add(L);
            spec.SaveImage("qrss-agc-off.png", intensity: 3);
        }

        [Test]
        public void Test_AGC_normToNoiseFloor()
        {
            // strategy here is to normalize to the magnitude of the quietest 20% of frequencies

            string wavFilePath = "../../../../../data/qrss-10min.wav";
            (int sampleRate, double[] L) = WavFile.ReadMono(wavFilePath);

            int fftSize = 8192;
            var spec = new SGram(sampleRate, fftSize, stepSize: 2000, maxFreq: 3000);
            spec.Add(L);

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

            spec.SaveImage("qrss-agc-norm-floor.png", intensity: 3);
        }

        [Test]
        public void Test_AGC_normWindow()
        {
            // strategy here is to create a weighted moving window mean and normalize to that

            string wavFilePath = "../../../../../data/qrss-10min.wav";
            (int sampleRate, double[] L) = WavFile.ReadMono(wavFilePath);

            int fftSize = 8192;
            var spec = new SGram(sampleRate, fftSize, stepSize: 2000, maxFreq: 3000);
            spec.Add(L);

            var ffts = spec.GetFFTs();
            for (int i = 0; i < ffts.Count; i++)
                ffts[i] = SubtractMovingWindowFloor(ffts[i]);

            spec.SaveImage("qrss-agc-norm-window.png", intensity: 3);
        }

        private double[] SubtractMovingWindow(double[] input, int windowSizePx = 100)
        {
            // return a copy of the input array with the moving window subtracted

            double[] window = FftSharp.Window.Hanning(windowSizePx);
            double windowSum = window.Sum();

            double[] windowed = new double[input.Length];
            double[] normalized = new double[input.Length];

            for (int i = 0; i < input.Length - window.Length; i++)
            {
                double windowedInputSum = 0;
                for (int j = 0; j < window.Length; j++)
                    windowedInputSum += input[i + j] * window[j];
                windowed[i + window.Length / 2] = windowedInputSum / windowSum;
            }

            for (int i = 0; i < input.Length; i++)
                normalized[i] = Math.Max(input[i] - windowed[i], 0);

            return normalized;
        }

        private double[] SubtractMovingWindowFloor(double[] input, int windowSizePx = 20, double percentile = .2)
        {
            // return a copy of the input with the noise floor subtracted
            // where the noise floor is calculated from a moving window
            // this is good but very slow

            double[] normalized = new double[input.Length];
            int floorIndex = (int)(percentile * windowSizePx);

            double[] segment = new double[windowSizePx];
            for (int i = 0; i < input.Length - windowSizePx; i++)
            {
                for (int j = 0; j < windowSizePx; j++)
                    segment[j] = input[i + j];
                Array.Sort(segment);
                normalized[i] = Math.Max(input[i] - segment[floorIndex], 0);
            }

            return normalized;
        }
    }
}
