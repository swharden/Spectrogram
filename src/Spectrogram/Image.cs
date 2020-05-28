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
        public static Bitmap Create(List<byte[]> pixelValues, IColormap colormap)
        {
            if (pixelValues is null || pixelValues.Count == 0)
                return null;

            int height = pixelValues[0].Length;
            int width = pixelValues.Count;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            colormap.Apply(bmp);

            var rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            byte[] pixels = new byte[bitmapData.Stride * bmp.Height];

            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    int bytePosition = (bmp.Height - 1 - row) * bitmapData.Stride + col;
                    pixels[bytePosition] = pixelValues[col][row];
                }
            }

            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }
    }
}
