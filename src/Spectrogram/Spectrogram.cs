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
        public readonly int? fixedWidth;

        public readonly float[] latestChunk;

        public double lastRenderMsec;

        public readonly List<float[]> ffts = new List<float[]>();
        public readonly List<float> signal = new List<float>();

        public Spectrogram(int sampleRate = 8000, int fftSize = 1024, int stepSize = 500, int? fixedWidth = null)
        {
            if (!Operations.IsPowerOfTwo(fftSize))
                throw new ArgumentException("FFT Size must be a power of 2");

            this.sampleRate = sampleRate;
            this.fftSize = fftSize;
            this.stepSize = stepSize;
            this.fixedWidth = fixedWidth;

            latestChunk = new float[fftSize];
        }

        public override string ToString()
        {
            return $"Spectrogram ({sampleRate} Hz) with {ffts.Count} segments ({fftSize} points each)";
        }

        public void Clear()
        {
            ffts.Clear();
        }

        public void SignalExtend(float[] values)
        {
            signal.AddRange(values);
            ProcessFFT();
        }

        public void ProcessFFT()
        {
            float[] oldestSegment = new float[fftSize];
            while (signal.Count > fftSize)
            {
                signal.CopyTo(0, oldestSegment, 0, fftSize);
                signal.RemoveRange(0, stepSize);
                ffts.Add(Operations.FFT(oldestSegment));

                if ((fixedWidth != null) && (ffts.Count >= fixedWidth))
                    ffts.RemoveRange(0, ffts.Count - (int)fixedWidth);
            }
        }

        public Bitmap GetBitmap()
        {
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
            Bitmap bmp = Image.BitmapFromFFTs(ffts, fixedWidth);
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
