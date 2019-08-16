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

        public readonly float[] latestChunk;

        readonly List<float[]> ffts = new List<float[]>();

        public Spectrogram(int sampleRate = 8000, int fftSize = 4096, int stepSize = 500)
        {
            if (!Operations.IsPowerOfTwo(fftSize))
                throw new ArgumentException("FFT Size must be a power of 2");

            this.sampleRate = sampleRate;
            this.fftSize = fftSize;
            this.stepSize = stepSize;

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

        public void Add(float[] values)
        {
            int stepCount = (values.Length - fftSize) / stepSize;
            
            for (int i=0; i<stepCount; i++)
            {
                Array.Copy(values, i * stepSize, latestChunk, 0, fftSize);
                ffts.Add(Operations.FFT(latestChunk));
            }

            Console.WriteLine($"Finished adding {values.Length} new values ({stepCount} steps)");
        }

        public void SaveBitmap(string fileName = "spectrograph.png")
        {
            string filePath = System.IO.Path.GetFullPath(fileName);
            Bitmap bmp = Image.BitmapFromFFTs(ffts);
            bmp.Save(filePath);
            Console.WriteLine($"Saved: {filePath}");
        }
    }
}
