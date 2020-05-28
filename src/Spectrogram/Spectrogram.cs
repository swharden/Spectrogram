using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;

namespace Spectrogram
{
    public class Spectrogram
    {
        public readonly int fftSize;
        public readonly int sampleRate;
        public readonly double[] freqs;
        private readonly double[] window;
        private readonly List<byte[]> pixelColumns = new List<byte[]>();
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
            Debug.WriteLine($"Keeping FFT index {fftKeepIndex1} to index {fftKeepIndex2} (of {fftSize / 2})");
        }

        public void Window(double[] newWindow)
        {
            if (newWindow.Length != fftSize)
                throw new ArgumentException("Window length must equal fftSize");
            Array.Copy(newWindow, 0, window, 0, fftSize);
        }

        public void AddSignal(double[] newValues)
        {
            incomingSignal.AddRange(newValues);
        }

        [Obsolete("the overload that accepts double[] is faster")]
        public void AddSignal(float[] newValues)
        {
            var a = newValues.Select(x => (double)x).ToArray();
            incomingSignal.AddRange(a);
        }

        public void ProcessAll(int stepSize, double pixelMult, double pixelOffset, double magOffset = 1)
        {
            while (incomingSignal.Count >= fftSize)
                ProcessNext(stepSize, pixelMult, pixelOffset, magOffset);
        }

        public void ProcessNext(int stepSize, double pixelMult, double pixelOffset, double magOffset = 1)
        {
            if (incomingSignal.Count < fftSize)
                return;

            // copy the oldest part of the incoming signal into the complex buffer
            for (int i = 0; i < fftSize; i++)
                buffer[i] = new Complex(incomingSignal[i] * window[i], 0);

            // crunch the FFT
            FftSharp.Transform.FFT(buffer);

            // calculate PSD magnitude, scale it to Decibels, convert it to pixel intensity
            byte[] pixelIntensity = new byte[fftSizeKeep];
            for (int i = 0; i < fftSizeKeep; i++)
            {
                double mag = buffer[fftKeepIndex1 + i].Magnitude + magOffset;
                double intensity = 20 * (float)Math.Log10(mag);
                intensity += pixelOffset;
                intensity *= pixelMult;
                intensity = Math.Min(intensity, 255);
                intensity = Math.Max(intensity, 0);
                pixelIntensity[i] = (byte)intensity;
            }
            pixelColumns.Add(pixelIntensity);

            // trim the start of the incoming signal by the step size
            if (incomingSignal.Count > stepSize)
                incomingSignal.RemoveRange(0, stepSize);
        }

        public Bitmap GetBitmap()
        {
            Bitmap bmp = Image.Create(pixelColumns, cmap);
            return bmp;
        }

        public void SavePNG(string saveFilePath)
        {
            GetBitmap().Save(saveFilePath, ImageFormat.Png);
        }

        public void SaveBMP(string saveFilePath)
        {
            GetBitmap().Save(saveFilePath, ImageFormat.Bmp);
        }

        public void SaveJPG(string saveFilePath)
        {
            GetBitmap().Save(saveFilePath, ImageFormat.Jpeg);
        }
    }
}
