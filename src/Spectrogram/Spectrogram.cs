using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram
{
    public class Spectrogram
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

        public Spectrogram(int sampleRate, int fftSize, int stepSize,
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

            // determine the bin locations (on the Mel scale)
            double maxMel = 2595 * Math.Log10(1 + settings.FreqMax / 700);
            double[] binStartFreqs = new double[melBinCount + 1];
            for (int i = 0; i < melBinCount + 1; i++)
            {
                double thisMel = maxMel * i / melBinCount;
                binStartFreqs[i] = 700 * (Math.Pow(10, thisMel / 2595d) - 1);
            }

            // calculate mel FFT for each FFT
            var fftsMel = new List<double[]>();
            for (int fftIndex = 0; fftIndex < Width; fftIndex++)
            {
                double[] thisFftMel = new double[melBinCount];
                for (int binIndex = 0; binIndex < binStartFreqs.Length - 2; binIndex++)
                {
                    double freqLow = binStartFreqs[binIndex];
                    double freqHigh = binStartFreqs[binIndex + 2];
                    int indexLow = (int)(Height * freqLow / settings.FreqMax);
                    int indexHigh = (int)(Height * freqHigh / settings.FreqMax);
                    int indexSpan = indexHigh - indexLow;

                    double binScaleSum = 0;
                    for (int i = 0; i < indexSpan; i++)
                    {
                        double frac = (double)i / indexSpan;
                        frac = (frac < .5) ? frac * 2 : 1 - frac;
                        binScaleSum += frac;
                        thisFftMel[binIndex] += ffts[fftIndex][indexLow + i] * frac;
                    }
                    thisFftMel[binIndex] /= binScaleSum;
                }
                fftsMel.Add(thisFftMel);
            }

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
    }
}
