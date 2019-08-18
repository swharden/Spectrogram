using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spectrogram
{
    public class Spectrogram
    {
        public readonly int fftSize;
        public readonly int sampleRate;

        public int nextIndex;
        public double lastRenderMsec;

        public readonly List<float[]> ffts = new List<float[]>();
        public readonly List<float> signal = new List<float>();

        public Spectrogram(
            int sampleRate = 8000,
            int fftSize = 1024,
            int? fixedSize = null
            )
        {
            if (!Operations.IsPowerOfTwo(fftSize))
                throw new ArgumentException("FFT Size must be a power of 2");

            if ((fixedSize != null) && (fixedSize < 1))
                throw new ArgumentException("size must be at least 1");

            this.sampleRate = sampleRate;
            this.fftSize = fftSize;

            if (fixedSize != null)
                while (ffts.Count < fixedSize)
                    ffts.Add(null);
        }

        public override string ToString()
        {
            return $"Spectrogram ({sampleRate} Hz) with {ffts.Count} segments ({fftSize} points each)";
        }

        public string GetConfigDetails()
        {
            double maxFreq = sampleRate / 2;
            int fftOutputPoints = fftSize / 2;
            double fftResolution = maxFreq / fftOutputPoints;

            string msg = "";
            msg += $"Sample rate: {sampleRate} Hz\n";
            msg += $"Maximum visible Frequency: {maxFreq} Hz\n";
            msg += $"FFT Size: {fftOutputPoints} points\n";
            msg += $"FFT Resolution: {fftResolution} Hz\n";

            return msg.Trim();
        }

        public int GetFftIndex(double frequency)
        {
            double maxFreq = sampleRate / 2;
            int fftOutputPoints = fftSize / 2;
            double fftResolution = maxFreq / fftOutputPoints;
            return (int)(frequency / fftResolution);
        }

        public void Add(float[] values, bool process = true, bool scroll = false, int stepSize = 500, int? fixedSize = null)
        {

            if (scroll && fixedSize == null)
                throw new ArgumentException("scroll requires a fixed size");

            signal.AddRange(values);

            if (process)
                ProcessNewSegments(scroll, stepSize, fixedSize);
        }

        public void ProcessNewSegments(bool scroll, int stepSize, int? fixedSize)
        {
            int segmentsNeedingProcessing = (signal.Count - fftSize) / stepSize;
            float[] nextSegment = new float[fftSize];

            while (signal.Count > (fftSize + stepSize))
            {

                int remainingSegments = (signal.Count - fftSize) / stepSize;
                if (remainingSegments % 10 == 0)
                {
                    Console.WriteLine(string.Format("Processing segment {0} of {1} ({2:0.0}%)",
                        ffts.Count + 1, segmentsNeedingProcessing, 100.0 * (ffts.Count + 1) / segmentsNeedingProcessing));
                }

                signal.CopyTo(0, nextSegment, 0, fftSize);
                signal.RemoveRange(0, stepSize);

                float[] fft = Operations.FFT(nextSegment);

                if (fixedSize == null)
                {
                    // extend the collection
                    ffts.Add(fft);
                }
                else
                {
                    while (ffts.Count < fixedSize)
                        ffts.Add(null);
                    while (ffts.Count > fixedSize)
                        ffts.RemoveAt(ffts.Count - 1);

                    if (scroll)
                    {
                        // add to the end and remove from the beginning
                        ffts.Add(fft);
                        ffts.RemoveAt(0);
                    }
                    else
                    {
                        // add new FFT at nextIndex
                        nextIndex = Math.Min(nextIndex, ffts.Count - 1);
                        ffts[nextIndex] = fft;
                        nextIndex += 1;
                        if (nextIndex >= ffts.Count)
                            nextIndex = 0;
                    }
                }

            }
        }

        private List<float[]> GetScrolledFFTs()
        {
            List<float[]> scrolled = new List<float[]>();
            scrolled.AddRange(ffts.GetRange(nextIndex, ffts.Count - nextIndex));
            scrolled.AddRange(ffts.GetRange(0, nextIndex));
            return scrolled;
        }

        public Bitmap GetBitmap(
            float intensity = 10,
            bool decibels = false,
            int? pixelUpper = null,
            int? pixelLower = null,
            bool vertical = false,
            bool scroll = false
            )
        {

            if (ffts.Count == 0)
                return null;

            var benchmark = new Benchmark();

            List<float[]> fftsToAnalyze = ffts;
            if (scroll)
                fftsToAnalyze = GetScrolledFFTs();

            Bitmap bmp = Image.BitmapFromFFTs(fftsToAnalyze, pixelLower, pixelUpper, intensity, decibels);

            if (vertical)
                bmp = Image.Rotate(bmp);

            lastRenderMsec = benchmark.elapsedMilliseconds;

            return bmp;
        }

        public void SaveBitmap(Bitmap bmp, string fileName = "spectrograph.png")
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
