using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Text;

namespace Spectrogram
{
    /* code is intentionally duplicated here (similar to SpectrogramLive) for effeciency */
    public class Spectrogram
    {
        private readonly double[,] mags;
        private readonly byte[,] pixels;
        private IColormap cmap = new Viridis();

        /// <summary>
        /// Create a Spectrogram from a pre-recorded signal.
        /// </summary>
        public Spectrogram(double[] signal, int sampleRate, int fftSize, int stepSize,
            double freqMax = double.PositiveInfinity, double freqMin = 0,
            double[] window = null, double multiplier = 1, double offset = 0, bool dB = false)
        {
            Complex[] buffer = new Complex[fftSize];

            if (window is null)
                window = FftSharp.Window.Hanning(fftSize);
            else if (window.Length != fftSize)
                throw new ArgumentException("If a window is given its length must equal fftSize");

            (int fftIndex1, int fftIndex2) = Calculate.FftIndexes(freqMin, freqMax, sampleRate, fftSize);
            int fftKeepSize = fftIndex2 - fftIndex1;
            Debug.WriteLine($"Keeping FFT index {fftIndex1} to index {fftIndex2} (from {fftSize / 2} points)");

            // determine how large the spectrogram will be and create it in memory
            int stepCount = (signal.Length - fftSize) / stepSize;
            mags = new double[stepCount, fftKeepSize];
            pixels = new byte[stepCount, fftKeepSize];

            for (int stepIndex = 0; stepIndex < stepCount; stepIndex++)
            {
                // copy signal info buffer
                for (int i = 0; i < fftSize; i++)
                    buffer[i] = new Complex(signal[stepIndex * stepSize + i] * window[i], 0);

                // crunch the FFT
                FftSharp.Transform.FFT(buffer);

                // calculate PSD magnitude (no scaling or conversion)
                for (int i = 0; i < fftKeepSize; i++)
                    mags[stepIndex, i] = buffer[fftIndex1 + i].Magnitude;
            }

            Recalculate(multiplier, offset, dB);
        }

        public void Recalculate(double multiplier, double offset = 0, bool dB = false, double dBoffset = 1, IColormap cmap = null)
        {
            if (cmap != null)
                this.cmap = cmap;

            for (int col = 0; col < mags.GetLength(0); col++)
            {
                for (int row = 0; row < mags.GetLength(1); row++)
                {
                    double value = mags[col, row];
                    if (dB)
                        value = 20 * Math.Log10(value + dBoffset);
                    value = (value + offset) * multiplier;
                    value = Math.Min(value, 255);
                    value = Math.Max(value, 0);
                    pixels[col, row] = (byte)value;
                }
            }
        }

        public Bitmap GetBitmap() => Image.Create(pixels, cmap);
        public void SavePNG(string saveFilePath) => GetBitmap().Save(saveFilePath, ImageFormat.Png);
        public void SaveBMP(string saveFilePath) => GetBitmap().Save(saveFilePath, ImageFormat.Bmp);
        public void SaveJPG(string saveFilePath) => GetBitmap().Save(saveFilePath, ImageFormat.Jpeg);
    }
}
