using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace Spectrogram
{
    public static class Image
    {
        public static Bitmap Create(List<float[]> ffts, IColormap colormap, float multiply, float offset = 0)
        {
            if (ffts is null || ffts.Count == 0)
                throw new ArgumentException("input must contain data");

            int height = ffts[0].Length;
            int width = ffts.Count;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            colormap.Apply(bmp);

            var rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            byte[] pixels = new byte[bitmapData.Stride * bmp.Height];

            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    float pixelValue = (ffts[col][row] + offset) * multiply;
                    pixelValue = Math.Max(0, pixelValue);
                    pixelValue = Math.Min(255, pixelValue);

                    int bytePosition = (bmp.Height - 1 - row) * bitmapData.Stride + col;
                    pixels[bytePosition] = (byte)pixelValue;
                }
            }

            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }
    }
}
