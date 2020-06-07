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
        public static Bitmap Create(byte[,] pixelValues, int wrapIndex = 0)
        {
            int height = pixelValues.GetLength(1);
            int width = pixelValues.GetLength(0);

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            var lockRect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = bmp.LockBits(lockRect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            int stride = bitmapData.Stride;

            byte[] bytes = new byte[bitmapData.Stride * height];
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

        public static Bitmap CreateMax(byte[,] pixelValues, int wrapIndex = 0, int pxPerPx = 1)
        {
            int height = pixelValues.GetLength(1) / pxPerPx;
            int width = pixelValues.GetLength(0);

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format8bppIndexed);

            var lockRect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = bmp.LockBits(lockRect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            int stride = bitmapData.Stride;

            byte[] bytes = new byte[bitmapData.Stride * height];
            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    int bytePosition = (height - 1 - row) * stride + col;
                    for (int i = 0; i < pxPerPx; i++)
                        bytes[bytePosition] = Math.Max(bytes[bytePosition], pixelValues[col, row * pxPerPx + i]);
                }
            }

            Marshal.Copy(bytes, 0, bitmapData.Scan0, bytes.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }
    }
}
