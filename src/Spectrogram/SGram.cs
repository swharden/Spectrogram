﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Spectrogram
{
    /// <summary>
    /// Spectrogram generator.
    /// Instantiate with FFT settings, use Add() methods to add signal data, then GetBitmap() or GetFfts()
    /// </summary>
    public class SGram
    {
        public int Width { get { return ffts.Count; } }
        public int Height { get { return settings.Height; } }
        public int FftSize { get { return settings.FftSize; } }
        public double HzPerPx { get { return settings.HzPerPixel; } }
        public double SecPerPx { get { return settings.StepLengthSec; } }
        public int FftsToProcess { get { return (newAudio.Count - settings.FftSize) / settings.StepSize; } }
        public int FftsProcessed { get; private set; }
        public int NextColumnIndex { get { return (FftsProcessed + rollOffset) % Width; } }
        public int OffsetHz { get { return settings.OffsetHz; } set { settings.OffsetHz = value; } }
        public int SampleRate { get { return settings.SampleRate; } }
        public int StepSize { get { return settings.StepSize; } }
        public double FreqMax { get { return settings.FreqMax; } }
        public double FreqMin { get { return settings.FreqMin; } }

        private readonly Settings settings;
        private readonly List<double[]> ffts = new List<double[]>();
        private readonly List<double> newAudio = new List<double>();
        private Colormap cmap = Colormap.Viridis;

        public SGram(int sampleRate, int fftSize, int stepSize,
            double minFreq = 0, double maxFreq = double.PositiveInfinity,
            int? fixedWidth = null, int offsetHz = 0)
        {
            settings = new Settings(sampleRate, fftSize, stepSize, minFreq, maxFreq, offsetHz);

            if (fixedWidth.HasValue)
                SetFixedWidth(fixedWidth.Value);
        }

        public override string ToString()
        {
            double processedSamples = ffts.Count * settings.StepSize + settings.FftSize;
            double processedSec = processedSamples / settings.SampleRate;
            string processedTime = (processedSec < 60) ? $"{processedSec:N2} sec" : $"{processedSec / 60.0:N2} min";

            return $"Spectrogram ({Width}, {Height})" +
                   $"\n  Vertical ({Height} px): " +
                   $"{settings.FreqMin:N0} - {settings.FreqMax:N0} Hz, " +
                   $"FFT size: {settings.FftSize:N0} samples, " +
                   $"{settings.HzPerPixel:N2} Hz/px" +
                   $"\n  Horizontal ({Width} px): " +
                   $"{processedTime}, " +
                   $"window: {settings.FftLengthSec:N2} sec, " +
                   $"step: {settings.StepLengthSec:N2} sec, " +
                   $"overlap: {settings.StepOverlapFrac * 100:N0}%";
        }

        public void SetColormap(Colormap cmap)
        {
            this.cmap = cmap ?? this.cmap;
        }

        public void SetWindow(double[] newWindow)
        {
            if (newWindow.Length > settings.FftSize)
                throw new ArgumentException("window length cannot exceed FFT size");

            for (int i = 0; i < settings.FftSize; i++)
                settings.Window[i] = 0;

            int offset = (settings.FftSize - newWindow.Length) / 2;
            Array.Copy(newWindow, 0, settings.Window, offset, newWindow.Length);
        }

        [Obsolete("use the Add() method", true)]
        public void AddExtend(float[] values) { }

        [Obsolete("use the Add() method", true)]
        public void AddCircular(float[] values) { }

        [Obsolete("use the Add() method", true)]
        public void AddScroll(float[] values) { }

        public void Add(double[] audio, bool process = true)
        {
            newAudio.AddRange(audio);
            if (process)
                Process();
        }

        private int rollOffset = 0;
        public void RollReset(int offset = 0)
        {
            rollOffset = -FftsProcessed + offset;
        }

        public double[][] Process()
        {
            if (FftsToProcess < 1)
                return null;

            int newFftCount = FftsToProcess;
            double[][] newFfts = new double[newFftCount][];

            Parallel.For(0, newFftCount, newFftIndex =>
            {
                FftSharp.Complex[] buffer = new FftSharp.Complex[settings.FftSize];
                int sourceIndex = newFftIndex * settings.StepSize;
                for (int i = 0; i < settings.FftSize; i++)
                    buffer[i].Real = newAudio[sourceIndex + i] * settings.Window[i];

                FftSharp.Transform.FFT(buffer);

                newFfts[newFftIndex] = new double[settings.Height];
                for (int i = 0; i < settings.Height; i++)
                    newFfts[newFftIndex][i] = buffer[settings.FftIndex1 + i].Magnitude / settings.FftSize;
            });

            foreach (var newFft in newFfts)
                ffts.Add(newFft);
            FftsProcessed += newFfts.Length;

            newAudio.RemoveRange(0, newFftCount * settings.StepSize);
            PadOrTrimForFixedWidth();

            return newFfts;
        }

        public List<double[]> GetMelFFTs(int melBinCount)
        {
            if (settings.FreqMin != 0)
                throw new InvalidOperationException("cannot get Mel spectrogram unless minimum frequency is 0Hz");

            var fftsMel = new List<double[]>();
            foreach (var fft in ffts)
                fftsMel.Add(FftSharp.Transform.MelScale(fft, SampleRate, melBinCount));

            return fftsMel;
        }

        public Bitmap GetBitmap(double intensity = 1, bool dB = false, bool roll = false) =>
            Image.GetBitmap(ffts, cmap, intensity, dB, roll, NextColumnIndex);

        public Bitmap GetBitmapMel(int melBinCount = 25, double intensity = 1, bool dB = false, bool roll = false) =>
            Image.GetBitmap(GetMelFFTs(melBinCount), cmap, intensity, dB, roll, NextColumnIndex);

        [Obsolete("use SaveImage()", true)]
        public void SaveBitmap(Bitmap bmp, string fileName) { }

        public void SaveImage(string fileName, double intensity = 1, bool dB = false, bool roll = false)
        {
            if (ffts.Count == 0)
                throw new InvalidOperationException("Spectrogram contains no data. Use Add() to add signal data.");

            string extension = Path.GetExtension(fileName).ToLower();

            ImageFormat fmt;
            if (extension == ".bmp")
                fmt = ImageFormat.Bmp;
            else if (extension == ".png")
                fmt = ImageFormat.Png;
            else if (extension == ".jpg" || extension == ".jpeg")
                fmt = ImageFormat.Jpeg;
            else if (extension == ".gif")
                fmt = ImageFormat.Gif;
            else
                throw new ArgumentException("unknown file extension");

            Image.GetBitmap(ffts, cmap, intensity, dB, roll, NextColumnIndex).Save(fileName, fmt);
        }

        public Bitmap GetBitmapMax(double intensity = 1, bool dB = false, bool roll = false, int reduction = 4)
        {
            List<double[]> ffts2 = new List<double[]>();
            for (int i = 0; i < ffts.Count; i++)
            {
                double[] d1 = ffts[i];
                double[] d2 = new double[d1.Length / reduction];
                for (int j = 0; j < d2.Length; j++)
                    for (int k = 0; k < reduction; k++)
                        d2[j] = Math.Max(d2[j], d1[j * reduction + k]);
                ffts2.Add(d2);
            }
            return Image.GetBitmap(ffts2, cmap, intensity, dB, roll, NextColumnIndex);
        }

        public void SaveData(string filePath, int melBinCount = 0)
        {
            if (!filePath.EndsWith(".sff", StringComparison.OrdinalIgnoreCase))
                filePath += ".sff";
            new SFF(this, melBinCount).Save(filePath);
        }

        private int fixedWidth = 0;
        public void SetFixedWidth(int width)
        {
            fixedWidth = width;
            PadOrTrimForFixedWidth();
        }

        private void PadOrTrimForFixedWidth()
        {
            if (fixedWidth > 0)
            {
                int overhang = Width - fixedWidth;
                if (overhang > 0)
                    ffts.RemoveRange(0, overhang);

                while (ffts.Count < fixedWidth)
                    ffts.Insert(0, new double[Height]);
            }
        }

        public Bitmap GetVerticalScale(int width, int offsetHz = 0, int tickSize = 3, int reduction = 1)
        {
            return Scale.Vertical(width, settings, offsetHz, tickSize, reduction);
        }

        public int PixelY(double frequency, int reduction = 1)
        {
            int pixelsFromZeroHz = (int)(settings.PxPerHz * frequency / reduction);
            int pixelsFromMinFreq = pixelsFromZeroHz - settings.FftIndex1 / reduction + 1;
            int pixelRow = settings.Height / reduction - 1 - pixelsFromMinFreq;
            return pixelRow - 1;
        }

        public List<double[]> GetFFTs()
        {
            return ffts;
        }

        public (double freqHz, double magRms) GetPeak(bool latestFft = true)
        {
            if (ffts.Count == 0)
                return (double.NaN, double.NaN);

            if (latestFft == false)
                throw new NotImplementedException("peak of mean of all FFTs not yet supported");

            double[] freqs = ffts[ffts.Count - 1];

            int peakIndex = 0;
            double peakMagnitude = 0;
            for (int i = 0; i < freqs.Length; i++)
            {
                if (freqs[i] > peakMagnitude)
                {
                    peakMagnitude = freqs[i];
                    peakIndex = i;
                }
            }

            double maxFreq = SampleRate / 2;
            double peakFreqFrac = peakIndex / (double)freqs.Length;
            double peakFreqHz = maxFreq * peakFreqFrac;

            return (peakFreqHz, peakMagnitude);
        }
    }
}
