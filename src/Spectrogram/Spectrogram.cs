using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spectrogram
{
    public class Spectrogram
    {
        private readonly Settings.FftSettings fftSettings;
        private readonly Settings.DisplaySettings displaySettings;

        public List<float[]> fftList = new List<float[]>();
        public List<float> signal = new List<float>();

        public int nextIndex;

        public Spectrogram(int sampleRate = 8000, int fftSize = 1024, int segmentSize = 200)
        {
            fftSettings = new Settings.FftSettings(sampleRate, fftSize, segmentSize);
            displaySettings = new Settings.DisplaySettings();
        }

        public override string ToString()
        {
            return $"Spectrogram ({fftSettings.sampleRate} Hz) " +
                "with {ffts.Count} segments in memory " +
                "({fftSettings.fftSize} points each)";
        }

        public string GetFftInfo()
        {
            return fftSettings.ToString();
        }

        public void AddExtend(float[] values)
        {
            signal.AddRange(values);
            ProcessNewSegments(scroll: false, fixedSize: null);
        }

        public void AddCircular(float[] values, int fixedSize)
        {
            signal.AddRange(values);
            ProcessNewSegments(scroll: false, fixedSize: fixedSize);
        }

        public void AddScroll(float[] values, int fixedSize)
        {
            signal.AddRange(values);
            ProcessNewSegments(scroll: true, fixedSize: fixedSize);
        }

        public void ProcessNewSegments(bool scroll, int? fixedSize)
        {
            int segmentsRemaining = (signal.Count - fftSettings.fftSize) / fftSettings.segmentSize;
            float[] segment = new float[fftSettings.fftSize];

            while (signal.Count > (fftSettings.fftSize + fftSettings.segmentSize))
            {

                int remainingSegments = (signal.Count - fftSettings.fftSize) / fftSettings.segmentSize;
                if (remainingSegments % 10 == 0)
                {
                    Console.WriteLine(string.Format("Processing segment {0} of {1} ({2:0.0}%)",
                        fftList.Count + 1, segmentsRemaining, 100.0 * (fftList.Count + 1) / segmentsRemaining));
                }

                signal.CopyTo(0, segment, 0, fftSettings.fftSize);
                signal.RemoveRange(0, fftSettings.segmentSize);

                float[] fft = Operations.FFT(segment);

                if (fixedSize == null)
                    FftListExtend(fft);
                else
                    FftListFixedSize(fft, (int)fixedSize, scroll);
            }
        }

        private void FftListExtend(float[] fft)
        {
            fftList.Add(fft);
        }

        private void FftListFixedSize(float[] fft, int fixedSize, bool scroll)
        {
            while (fftList.Count < fixedSize)
                fftList.Add(null);
            while (fftList.Count > fixedSize)
                fftList.RemoveAt(fftList.Count - 1);

            if (scroll)
            {
                // add to the end and remove from the beginning
                fftList.Add(fft);
                fftList.RemoveAt(0);
            }
            else
            {
                // add new FFT at nextIndex
                nextIndex = Math.Min(nextIndex, fftList.Count - 1);
                fftList[nextIndex] = fft;
                nextIndex += 1;
                if (nextIndex >= fftList.Count)
                    nextIndex = 0;
            }
        }

        private List<float[]> GetScrolledFFTs()
        {
            List<float[]> scrolled = new List<float[]>();
            scrolled.AddRange(fftList.GetRange(nextIndex, fftList.Count - nextIndex));
            scrolled.AddRange(fftList.GetRange(0, nextIndex));
            return scrolled;
        }

        public Bitmap GetBitmap(
            float intensity = 10,
            bool decibels = false,
            double frequencyMin = 0,
            double frequencyMax = double.MaxValue,
            bool vertical = false
            )
        {
            if (fftList.Count == 0)
                return null;
            if (frequencyMin < 0)
                throw new ArgumentException("frequencyMin must be greater than 0");
            if (frequencyMax < frequencyMin)
                throw new ArgumentException("frequencyMin must be less than frequencyMax");

            int pixelLower = fftSettings.IndexFromFrequency(frequencyMin);
            int pixelUpper = fftSettings.IndexFromFrequency(frequencyMax);
            if (pixelUpper > fftSettings.fftOutputPointCount)
                pixelUpper = fftSettings.fftOutputPointCount;
            if (pixelUpper - pixelLower < 1)
                throw new ArgumentException("FFT frequency range is too small");

            Bitmap bmp;
            using (var benchmark = new Benchmark())
            {
                bmp = Image.BitmapFromFFTs(fftList.ToArray(), pixelLower, pixelUpper, intensity, decibels);
                if (vertical)
                    bmp = Image.Rotate(bmp);
                displaySettings.lastRenderMsec = benchmark.elapsedMilliseconds;
            }

            return bmp;
        }

        public double GetLastRenderTime()
        {
            return displaySettings.lastRenderMsec;
        }

        public void SaveBitmap(Bitmap bmp, string fileName = "spectrogram.png")
        {
            string filePath = System.IO.Path.GetFullPath(fileName);
            string extension = System.IO.Path.GetExtension(fileName).ToUpper();

            var imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
            if (extension == ".JPG" || extension == ".JPEG")
                imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            else if (extension == ".PNG")
                imageFormat = System.Drawing.Imaging.ImageFormat.Png;
            else if (extension == ".TIF" || extension == ".TIFF")
                imageFormat = System.Drawing.Imaging.ImageFormat.Tiff;

            bmp.Save(filePath, imageFormat);
            Console.WriteLine($"Saved: {filePath}");
        }
    }
}
