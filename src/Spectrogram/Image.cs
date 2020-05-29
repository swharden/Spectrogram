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

        public static Bitmap Create(byte[,] pixelValues, IColormap colormap)
        {
            if (pixelValues is null || pixelValues.GetLength(0) == 0)
                return null;

            int height = pixelValues.GetLength(1);
            int width = pixelValues.GetLength(0);

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
                    pixels[bytePosition] = pixelValues[col, row];
                }
            }

            Marshal.Copy(pixels, 0, bitmapData.Scan0, pixels.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }

        public static void CmapToPng(IColormap cmap, string saveAs, int pxPerValue = 1, int height = 50)
        {
            // TODO: move this inside Colormap class?

            Bitmap bmp = new Bitmap(pxPerValue * 256, height);
            Graphics gfx = Graphics.FromImage(bmp);
            for (int i = 0; i < 256; i++)
            {
                var (r, g, b) = cmap.Lookup(i);
                Brush brush = new SolidBrush(Color.FromArgb(255, r, g, b));
                gfx.FillRectangle(brush, i * pxPerValue, 0, pxPerValue, height);
            }
            bmp.Save(saveAs, ImageFormat.Png);
        }
    }
}
