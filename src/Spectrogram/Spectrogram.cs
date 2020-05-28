using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Spectrogram
{
    public class Spectrogram
    {
        public readonly int fftSize;
        public readonly int sampleRate;
        public readonly double[] freqs;
        private readonly double[] window;
        private readonly List<float[]> psds = new List<float[]>();
        private readonly List<double> incomingSignal = new List<double>();
        private readonly Complex[] buffer;
        private readonly IColormap cmap;
        private readonly int fftSizeKeep, fftKeepIndex2, fftKeepIndex1;

        public Spectrogram(int fftSize, int sampleRate, double freqMax = double.PositiveInfinity, double freqMin = 0)
        {
            if (fftSize < 2 || !FftSharp.Transform.IsPowerOfTwo(fftSize))
                throw new ArgumentException("FFT size must be a power of two");

            this.fftSize = fftSize;
            this.sampleRate = sampleRate;
            window = new double[fftSize];
            buffer = new Complex[fftSize];
            Window(FftSharp.Window.Hanning(fftSize));
            freqs = FftSharp.Transform.FFTfreq(sampleRate, fftSize / 2);
            cmap = new Colormaps.Viridis();

            fftKeepIndex2 = fftSize / 2;
            for (int i = 0; i < fftSize / 2; i++)
                if (freqMax >= freqs[i])
                    fftKeepIndex2 = i;

            fftKeepIndex1 = 0;
            for (int i = fftSize / 2 - 1; i >= 0; i--)
                if (freqMin <= freqs[i])
                    fftKeepIndex1 = i;

            fftSizeKeep = fftKeepIndex2 - fftKeepIndex1;
            Console.WriteLine($"Keeping FFT point {fftKeepIndex1} to point {fftKeepIndex2} (of {fftSize / 2})");
        }

        public void Window(double[] newWindow)
        {
            if (newWindow.Length != fftSize)
                throw new ArgumentException("Window length must equal fftSize");
            Array.Copy(newWindow, 0, window, 0, fftSize);
        }

        public void AddSignal(double[] newValues, bool process = true)
        {
            incomingSignal.AddRange(newValues);
        }

        public void AddSignal(float[] newValues)
        {
            var a = newValues.Select(x => (double)x).ToArray();
            incomingSignal.AddRange(a);
        }

        public void ProcessAll(int stepSize)
        {
            while (incomingSignal.Count >= fftSize)
                ProcessNext(stepSize);
        }

        public void ProcessNext(int stepSize, double magnitudeOffset = 1)
        {
            if (incomingSignal.Count < fftSize)
                return;

            // copy the oldest part of the incoming signal into the complex buffer
            for (int i = 0; i < fftSize; i++)
                buffer[i] = new Complex(incomingSignal[i] * window[i], 0);

            // crunch the FFT
            FftSharp.Transform.FFT(buffer);

            // collect the magnitude of the positive FFT and scale it to decibels
            float[] psd = new float[fftSizeKeep];
            for (int i = 0; i < fftSizeKeep; i++)
                psd[i] = 20 * (float)Math.Log10(buffer[fftKeepIndex1 + i].Magnitude + magnitudeOffset);
            psds.Add(psd);

            // trim the start of the incoming signal by the step size
            incomingSignal.RemoveRange(0, stepSize);
        }

        public Bitmap GetBitmap(double multiplier = 1, double offset = 0)
        {
            Bitmap bmp = Image.Create(psds, cmap, (float)multiplier, (float)offset);
            return bmp;
        }

        public void SavePNG(string saveFilePath, double multiplier = 1, double offset = 0)
        {
            Bitmap bmp = GetBitmap(multiplier, offset);
            bmp.Save(saveFilePath, ImageFormat.Png);
        }

        public void SaveBMP(string saveFilePath, double multiplier = 1, double offset = 0)
        {
            Bitmap bmp = GetBitmap(multiplier, offset);
            bmp.Save(saveFilePath, ImageFormat.Bmp);
        }

        public void SaveJPG(string saveFilePath, double multiplier = 1, double offset = 0)
        {
            Bitmap bmp = GetBitmap(multiplier, offset);
            bmp.Save(saveFilePath, ImageFormat.Jpeg);
        }

        private (float min, float max) Stats(List<float[]> psds)
        {
            int width = psds.Count;
            int height = psds[0].Length;
            int count = psds[0].Length * psds.Count;

            Console.WriteLine($"Analyzing {count} values...");

            float[] allValues = new float[count];
            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    allValues[col * height + row] = psds[col][row];
                }
            }

            Array.Sort(allValues);
            float min = allValues[0];
            float max = allValues[count - 1];

            Console.WriteLine($"Width: {width}");
            Console.WriteLine($"Height: {height}");
            Console.WriteLine($"Values: {count}");
            Console.WriteLine();
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Max: {max}");

            double[] percentiles = new double[101];
            percentiles[0] = allValues[0];
            for (int i = 1; i < 100; i++)
                percentiles[i] = allValues[(int)(allValues.Length * i / 100.0)];
            percentiles[100] = allValues[allValues.Length - 1];
            for (int i=0; i<percentiles.Length; i++)
                Console.WriteLine($"{i} percentile: {percentiles[i]}");

            return (min, max);
        }
    }
}
