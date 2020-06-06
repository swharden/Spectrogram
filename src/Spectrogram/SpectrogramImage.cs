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
    public class SpectrogramImage
    {
        private readonly double[,] mags;
        private readonly byte[,] pixels;
        public IColormap cmap = new Viridis();

        /// <summary>
        /// Create a Spectrogram from a pre-recorded signal.
        /// </summary>
        public SpectrogramImage(double[] signal, int sampleRate, int fftSize, int stepSize,
            double freqMax = double.PositiveInfinity, double freqMin = 0,
            double[] window = null, double multiplier = 1, double offset = 0, bool dB = false)
        {
            Complex[] buffer = new Complex[fftSize];

            if (window is null)
                window = FftSharp.Window.Hanning(fftSize);

            window = ZeroPad(window, fftSize);

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
                    mags[stepIndex, i] = buffer[fftIndex1 + i].Magnitude / mags.GetLength(0);
            }

            Recalculate(multiplier, offset, dB);
        }

        public double[] ZeroPad(double[] input, int targetLength)
        {
            int difference = targetLength - input.Length;
            double[] padded = new double[targetLength];
            Array.Copy(input, 0, padded, difference / 2, input.Length);
            return padded;
        }

        public void Recalculate(double multiplier, double offset = 0, bool dB = false, IColormap cmap = null)
        {
            if (cmap != null)
                this.cmap = cmap;

            for (int col = 0; col < mags.GetLength(0); col++)
            {
                for (int row = 0; row < mags.GetLength(1); row++)
                {
                    double value = mags[col, row];
                    if (dB)
                        value = 20 * Math.Log10(value + 1);
                    value = (value + offset) * multiplier;
                    value = Math.Min(value, 255);
                    value = Math.Max(value, 0);
                    pixels[col, row] = (byte)value;
                }
            }
        }

        public double IdealMultiplier(double percentile = 95, bool dB = false)
        {
            if (percentile < 0 || percentile > 100)
                throw new ArgumentException("percentile must be 0-100");

            double[] flat = new double[mags.GetLength(0) * mags.GetLength(1)];
            for (int i = 0; i < mags.GetLength(0); i++)
                for (int j = 0; j < mags.GetLength(1); j++)
                    flat[i * j + j] = mags[i, j];
            Array.Sort(flat);

            double magnitude;
            if (percentile == 0)
                magnitude = flat[0];
            else if (percentile == 100)
                magnitude = flat[flat.Length - 1];
            else
                magnitude = flat[(int)(percentile / 100 * (flat.Length - 1))];

            if (dB)
                magnitude = 20 * Math.Log10(magnitude + 1);

            return 256 / magnitude;
        }

        public double[,] GetMags() => mags;

        public Bitmap GetBitmap() => Image.Create(pixels, cmap);
        public void SavePNG(string saveFilePath) => GetBitmap().Save(saveFilePath, ImageFormat.Png);
        public void SaveBMP(string saveFilePath) => GetBitmap().Save(saveFilePath, ImageFormat.Bmp);
        public void SaveJPG(string saveFilePath) => GetBitmap().Save(saveFilePath, ImageFormat.Jpeg);
        public void SaveBMPcompressed(string saveFilePath, int halfTimes = 1)
        {
            var compressedPixels = this.pixels;
            for (int i = 0; i < halfTimes; i++)
                compressedPixels = CompressVertMax(compressedPixels);
            Bitmap bmp = Image.Create(compressedPixels, cmap);
            bmp.Save(saveFilePath, ImageFormat.Bmp);
        }

        private byte[,] CompressVertMax(byte[,] input)
        {
            Console.WriteLine("compressing...");
            byte[,] output = new byte[input.GetLength(0), input.GetLength(1) / 2];
            for (int x = 0; x < output.GetLength(0); x++)
            {
                for (int y = 0; y < output.GetLength(1); y++)
                {
                    double value = (input[x, y * 2] + input[x, y * 2 + 1]) / 2;
                    output[x, y] = (byte)value;
                }
            }
            return output;
        }
    }
}
