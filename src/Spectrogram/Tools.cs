﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Spectrogram
{
    public static class Tools
    {
        [Obsolete("The SFF file format is obsolete. " +
            "Users are encouraged to write their own IO routines specific to their application. " +
            "To get a copy of the original SFF reader/writer see https://github.com/swharden/Spectrogram/issues/44",
            error: true)]
        /// <summary>
        /// Collapse the 2D spectrogram into a 1D array (mean power of each frequency)
        /// </summary>
        public static double[] SffMeanFFT(SFF sff, bool dB = false)
        {
            double[] mean = new double[sff.Ffts[0].Length];

            foreach (var fft in sff.Ffts)
                for (int y = 0; y < fft.Length; y++)
                    mean[y] += fft[y];

            for (int i = 0; i < mean.Length; i++)
                mean[i] /= sff.Ffts.Count();

            if (dB)
                for (int i = 0; i < mean.Length; i++)
                    mean[i] = 20 * Math.Log10(mean[i]);

            if (mean[mean.Length - 1] <= 0)
                mean[mean.Length - 1] = mean[mean.Length - 2];

            return mean;
        }


        [Obsolete("The SFF file format is obsolete. " +
            "Users are encouraged to write their own IO routines specific to their application. " +
            "To get a copy of the original SFF reader/writer see https://github.com/swharden/Spectrogram/issues/44",
            error: true)]
        /// <summary>
        /// Collapse the 2D spectrogram into a 1D array (mean power of each time point)
        /// </summary>
        public static double[] SffMeanPower(SFF sff, bool dB = false)
        {
            double[] power = new double[sff.Ffts.Count];

            for (int i = 0; i < sff.Ffts.Count; i++)
                power[i] = (double)sff.Ffts[i].Sum() / sff.Ffts[i].Length;

            if (dB)
                for (int i = 0; i < power.Length; i++)
                    power[i] = 20 * Math.Log10(power[i]);

            return power;
        }

        [Obsolete("The SFF file format is obsolete. " +
            "Users are encouraged to write their own IO routines specific to their application. " +
            "To get a copy of the original SFF reader/writer see https://github.com/swharden/Spectrogram/issues/44",
            error: true)]
        public static double GetPeakFrequency(SFF sff, bool firstFftOnly = false)
        {
            double[] freqs = firstFftOnly ? sff.Ffts[0] : SffMeanFFT(sff, false);

            int peakIndex = 0;
            double peakPower = 0;
            for (int i = 0; i < freqs.Length; i++)
            {
                if (freqs[i] > peakPower)
                {
                    peakPower = freqs[i];
                    peakIndex = i;
                }
            }

            double maxFreq = sff.SampleRate / 2;
            double frac = peakIndex / (double)sff.ImageHeight;

            if (sff.MelBinCount > 0)
            {
                double maxMel = FftSharp.Transform.MelFromFreq(maxFreq);
                return FftSharp.Transform.MelToFreq(frac * maxMel);
            }
            else
            {
                return frac * maxFreq;
            }
        }

        public static int GetPianoKey(double frequencyHz)
        {
            double pianoKey = (39.86 * Math.Log10(frequencyHz / 440)) + 49;
            return (int)Math.Round(pianoKey);
        }

        public static int GetMidiNote(double frequencyHz)
        {
            return GetPianoKey(frequencyHz) + 20;
        }

        public static Bitmap FftsToImage(double[,] ffts, double mult, IColormap cmap)
        {
            byte[,,] pixelArray = new byte[ffts.GetLength(1), ffts.GetLength(0), 3];
            for (int x = 0; x < pixelArray.GetLength(1); x++)
            {
                for (int y = 0; y < pixelArray.GetLength(0); y++)
                {
                    int y2 = pixelArray.GetLength(0) - y - 1;
                    double value = ffts[x, y] * mult;
                    byte clampedValue = (byte)Math.Min(255, Math.Max(0, value));
                    (byte r, byte g, byte b) = cmap.GetRGB(clampedValue);
                    pixelArray[y2, x, 0] = r;
                    pixelArray[y2, x, 1] = g;
                    pixelArray[y2, x, 2] = b;
                }
            }

            return ArrayToImage(pixelArray);
        }

        public static Bitmap ArrayToImage(byte[,,] pixelArray)
        {
            int width = pixelArray.GetLength(1);
            int height = pixelArray.GetLength(0);
            int stride = (width % 4 == 0) ? width : width + 4 - width % 4;
            int bytesPerPixel = 3;

            byte[] bytes = new byte[stride * height * bytesPerPixel];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int offset = (y * stride + x) * bytesPerPixel;
                    bytes[offset + 0] = pixelArray[y, x, 2]; // blue
                    bytes[offset + 1] = pixelArray[y, x, 1]; // green
                    bytes[offset + 2] = pixelArray[y, x, 0]; // red
                }
            }

            PixelFormat formatOutput = PixelFormat.Format24bppRgb;
            Rectangle rect = new(0, 0, width, height);
            Bitmap bmp = new(stride, height, formatOutput);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, formatOutput);
            Marshal.Copy(bytes, 0, bmpData.Scan0, bytes.Length);
            bmp.UnlockBits(bmpData);

            Bitmap bmp2 = new(width, height, PixelFormat.Format32bppPArgb);
            Graphics gfx2 = Graphics.FromImage(bmp2);
            gfx2.DrawImage(bmp, 0, 0);

            return bmp2;
        }
    }
}
