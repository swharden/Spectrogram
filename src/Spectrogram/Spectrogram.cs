using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spectrogram
{
    public class Spectrogram
    {
        public readonly int fftSize;
        public readonly int stepSize;
        public readonly int sampleRate;
        public float intensity;

        public int? fixedSize;
        public bool vertical;
        int? pixelLower = null;
        int? pixelUpper = null;

        public bool scroll;
        public int nextIndex;

        public readonly float[] latestSegment;

        public double lastRenderMsec;

        public readonly List<float[]> ffts = new List<float[]>();
        public readonly List<float> signal = new List<float>();

        public Spectrogram(
            int sampleRate = 8000,
            int fftSize = 1024,
            int stepSize = 500,
            int? fixedSize = null,
            bool vertical = false,
            bool scroll = false,
            int? pixelLower = null,
            int? pixelUpper = null,
            float intensity = 10
            )
        {
            if (!Operations.IsPowerOfTwo(fftSize))
                throw new ArgumentException("FFT Size must be a power of 2");

            if ((fixedSize != null) && (fixedSize < 1))
                throw new ArgumentException("size must be at least 1");

            if (scroll && fixedSize == null)
                throw new ArgumentException("scroll requires a fixed size");

            this.sampleRate = sampleRate;
            this.fftSize = fftSize;
            this.stepSize = stepSize;
            this.fixedSize = fixedSize;
            this.vertical = vertical;
            this.scroll = scroll;
            this.pixelLower = pixelLower;
            this.pixelUpper = pixelUpper;
            this.intensity = intensity;

            if (fixedSize != null)
                while (ffts.Count < fixedSize)
                    ffts.Add(null);

            latestSegment = new float[fftSize];
        }

        public override string ToString()
        {
            return $"Spectrogram ({sampleRate} Hz) with {ffts.Count} segments ({fftSize} points each)";
        }

        public void SignalExtend(float[] values, bool processToo = true)
        {
            signal.AddRange(values);
            if (processToo)
                ProcessFFT();
        }

        public void ProcessFFT()
        {
            float[] oldestSegment = new float[fftSize];
            while (signal.Count > (fftSize + stepSize))
            {
                int remainingSegments = (signal.Count - fftSize) / stepSize;
                //Console.WriteLine($"Processing segment #{ffts.Count + 1} ({remainingSegments} segments remain)");

                signal.CopyTo(0, oldestSegment, 0, fftSize);
                signal.RemoveRange(0, stepSize);

                float[] fft = Operations.FFT(oldestSegment);

                if (scroll)
                {
                    ffts.Add(fft);
                    ffts.RemoveAt(0);
                }
                else
                {
                    ffts[nextIndex] = fft;
                    nextIndex += 1;
                    if (nextIndex >= ffts.Count)
                        nextIndex = 0;
                }

            }
        }

        public Bitmap GetBitmap()
        {
            if (ffts.Count == 0)
                return null;

            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            int? verticalLine = null;
            if (!scroll)
                verticalLine = nextIndex;
            Bitmap bmp = Image.BitmapFromFFTs(ffts, fixedSize, verticalLine, pixelLower, pixelUpper, intensity);
            if (vertical)
                bmp = Image.Rotate(bmp);
            lastRenderMsec = stopwatch.ElapsedTicks * 1000.0 / System.Diagnostics.Stopwatch.Frequency;
            return bmp;
        }

        public void SaveBitmap(string fileName = "spectrograph.png")
        {
            string filePath = System.IO.Path.GetFullPath(fileName);
            string extension = System.IO.Path.GetExtension(fileName).ToUpper();

            var imageFormat = System.Drawing.Imaging.ImageFormat.Bmp;
            if (extension == "JPG" || extension == "JPEG")
                imageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
            else if (extension == "PNG")
                imageFormat = System.Drawing.Imaging.ImageFormat.Png;
            else if (extension == "TIF" || extension == "TIFF")
                imageFormat = System.Drawing.Imaging.ImageFormat.Tiff;

            Bitmap bmp = GetBitmap();
            bmp.Save(filePath, imageFormat);
            Console.WriteLine($"Saved: {filePath}");
        }
    }
}
