using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Spectrogram
{
    class Image
    {
        static void ApplyPaletteGrayscale(Bitmap bmp)
        {
            ColorPalette pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = Color.FromArgb(255, i, i, i);
            bmp.Palette = pal;
        }

        public static Bitmap BitmapFromFFTs(List<float[]> ffts)
        {

            if (ffts == null || ffts.Count == 0)
                throw new ArgumentException("ffts must contain float arrays");

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // use indexed colors to make it easy to convert from value to color
            Bitmap bmp = new Bitmap(ffts.Count, ffts[0].Length, PixelFormat.Format8bppIndexed);
            ApplyPaletteGrayscale(bmp);

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            byte[] pixels = new byte[bitmapData.Stride * bmp.Height];

            // TODO: smarter intensity scaling (adjustable gain?)
            float scaleMax = 0;
            foreach (float value in ffts[ffts.Count / 2])
                scaleMax = Math.Max(scaleMax, value);
            //scaleMax *= (float).2;

            for (int col = 0; col < ffts.Count; col++)
            {
                for (int row = 0; row < ffts[col].Length; row++)
                {
                    int bytePosition = (ffts[col].Length - 1 - row) * bitmapData.Stride + col;
                    float pixelValue = ffts[col][row] / scaleMax * 255;
                    pixelValue = Math.Max(0, pixelValue);
                    pixelValue = Math.Min(255, pixelValue);
                    pixels[bytePosition] = (byte)(pixelValue);
                }
            }

            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bmp.UnlockBits(bitmapData);

            double elapsedMsec = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            Console.WriteLine($"Created image ({bmp.Width} x {bmp.Height}) in {elapsedMsec} ms");

            return bmp;
        }
    }
}
