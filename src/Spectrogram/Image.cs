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
        public static Bitmap BitmapFromFFTs(
            List<float[]> ffts,
            int? pixelLow,
            int? pixelHigh,
            float intensity,
            bool decibels
            )
        {

            if (ffts == null || ffts.Count == 0)
                throw new ArgumentException("ffts must contain float arrays");

            int fftHeight;
            if (ffts[0] != null)
                fftHeight = ffts[0].Length;
            else if (ffts[ffts.Count - 1] != null)
                fftHeight = ffts[ffts.Count - 1].Length;
            else
                return null;

            if (pixelLow == null)
                pixelLow = 0;
            else
                pixelLow = Math.Max((int)pixelLow, 0);

            if (pixelHigh == null)
                pixelHigh = fftHeight;
            else
                pixelHigh = Math.Min((int)pixelHigh, fftHeight);

            if ((int)pixelHigh <= (int)pixelLow)
                throw new ArgumentException("pixelHigh must be greater than pixelLow");

            int height = (int)pixelHigh - (int)pixelLow;
            int width = ffts.Count;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            Palette.ApplyLUT(bmp, Palette.LUT.viridis);

            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            byte[] pixels = new byte[bitmapData.Stride * bmp.Height];

            for (int col = 0; col < bmp.Width; col++)
            {
                if (col >= width)
                    continue;

                if (ffts[col] == null)
                    continue;

                for (int row = 0; row < bmp.Height; row++)
                {
                    int bytePosition = (bmp.Height - 1 - row) * bitmapData.Stride + col;
                    float pixelValue;
                    pixelValue = ffts[col][row + (int)pixelLow];
                    if (decibels)
                        pixelValue = (float)(Math.Log10(pixelValue) * 20);
                    pixelValue = (pixelValue * intensity);
                    pixelValue = Math.Max(0, pixelValue);
                    pixelValue = Math.Min(255, pixelValue);
                    pixels[bytePosition] = (byte)(pixelValue);
                }
            }

            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }

        public static Bitmap Rotate(Bitmap bmpIn, float angle = 90)
        {
            // TODO: this could be faster with byte manipulation since it's 90 degrees

            if (bmpIn == null)
                return null;

            Bitmap bmp = new Bitmap(bmpIn);
            Bitmap bmpRotated = new Bitmap(bmp.Height, bmp.Width);

            Graphics gfx = Graphics.FromImage(bmpRotated);
            gfx.RotateTransform(angle);
            gfx.DrawImage(bmp, new Point(0, -bmp.Height));

            return bmpRotated;
        }
    }
}
