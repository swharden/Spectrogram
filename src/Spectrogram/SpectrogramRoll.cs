using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram
{
    /// <summary>
    /// Spectrogram optimized for fixed-with live data
    /// </summary>
    public class SpectrogramRoll
    {
        public readonly int FftSize;
        public readonly int SampleRate;

        public readonly double[] Audio;
        private readonly double[] Window;
        private readonly int[] stepOffsets;
        private readonly byte[,] pixelValues;

        private readonly int Width;
        private readonly int Height;

        public SpectrogramRoll(int sampleRate = 6000, int fftSize = 1 << 15, int audioLengthSec = 60 * 10)
        {
            SampleRate = sampleRate;
            FftSize = fftSize;
            Height = fftSize / 2;
            Window = FftSharp.Window.Hanning(FftSize);
            Audio = new double[audioLengthSec * sampleRate];

            // TODO: refine this
            int stepSize = (Audio.Length - fftSize) / 1000;
            int stepCount = (Audio.Length - fftSize) / stepSize;
            Width = stepCount;

            stepOffsets = new int[stepCount];
            for (int i = 0; i < stepCount; i++)
                stepOffsets[i] = i * stepSize;

            pixelValues = new byte[Width, Height];
        }

        public void ProcessAll()
        {
            Parallel.For(0, Width, x =>
            {
                var fftBuffer = new Complex[FftSize];

                for (int i = 0; i < FftSize; i++)
                    fftBuffer[i] = new Complex(Audio[stepOffsets[x] + i], 0);

                FftSharp.Transform.FFT(fftBuffer);

                for (int y = 0; y < FftSize / 2; y++)
                {
                    double value = fftBuffer[y].Magnitude / FftSize;
                    value = Math.Max(value, 0);
                    value = Math.Min(value, 255);
                    pixelValues[x, y] = (byte)value;
                }
            });
        }

        public Bitmap GetBitmap()
        {
            int width = pixelValues.GetLength(0);
            int height = pixelValues.GetLength(1);
            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            // grayscale
            ColorPalette pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(255, i, i, i);
            bmp.Palette = pal;

            var lockRect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = bmp.LockBits(lockRect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            int stride = bitmapData.Stride;

            byte[] bytes = new byte[bitmapData.Stride * bmp.Height];
            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    int bytePosition = (height - 1 - row) * stride + col;
                    bytes[bytePosition] = pixelValues[col, row];
                }
            }

            Marshal.Copy(bytes, 0, bitmapData.Scan0, bytes.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }
    }
}
