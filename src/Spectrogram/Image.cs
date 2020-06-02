using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace Spectrogram
{
    public static class Image
    {
        public static Bitmap Create(byte[,] pixelValues, IColormap colormap, int wrapIndex = 0, int highlightColumnIndex = -1)
        {
            if (pixelValues is null || pixelValues.GetLength(0) == 0)
                throw new ArgumentException();

            int height = pixelValues.GetLength(1);
            int width = pixelValues.GetLength(0);

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            colormap.Apply(bmp);

            var lockRect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = bmp.LockBits(lockRect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            int stride = bitmapData.Stride;

            // TODO: optimize spectrogram class to use flat (not 2D) array
            byte[] bytes = new byte[bitmapData.Stride * bmp.Height];
            for (int col = 0; col < width; col++)
            {
                int sourceCol = col + wrapIndex;
                if (sourceCol >= width)
                    sourceCol -= width;

                for (int row = 0; row < height; row++)
                {
                    int bytePosition = (height - 1 - row) * stride + col;
                    bytes[bytePosition] = pixelValues[sourceCol, row];
                }
            }

            if (highlightColumnIndex >=0 && highlightColumnIndex < width)
            {
                for (int row = 0; row < height; row++)
                {
                    int bytePosition = (height - 1 - row) * stride + highlightColumnIndex;
                    bytes[bytePosition] = 255;
                }
            }

            Marshal.Copy(bytes, 0, bitmapData.Scan0, bytes.Length);
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
