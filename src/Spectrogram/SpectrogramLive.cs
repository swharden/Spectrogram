using Spectrogram.Colormaps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace Spectrogram
{
    public class SpectrogramLive
    {
        public readonly int sampleRate;
        public readonly double fftResolution;
        public readonly double fftFreqSpacing;
        public readonly int fftSize;
        public readonly int stepSize;

        private readonly Complex[] buffer;

        public readonly int fftIndex1;
        public readonly int fftIndex2;
        public readonly int fftKeepSize;

        private readonly List<double> signal = new List<double>();
        private readonly byte[,] pixelValues;
        private int nextColumnIndex = 0;
        private int lastColumnIndex = 0;
        public readonly double[] lastFft;

        public int fftsProcessed { get; private set; }

        // customizable settings
        private double[] window;
        private double dbMagOffset = 0;
        public bool dB = false;
        private double pixelOffset = 0;
        public double pixelMult = 1;
        public IColormap cmap = new Viridis();

        public SpectrogramLive(int sampleRate, int fftSize, int stepSize, int width = 100,
            double freqMax = double.PositiveInfinity, double freqMin = 0)
        {
            if (FftSharp.Transform.IsPowerOfTwo(fftSize) == false)
                throw new ArgumentException("fftSize must be a power of 2");

            this.sampleRate = sampleRate;
            this.fftSize = fftSize;
            this.stepSize = stepSize;
            fftResolution = (double)sampleRate / fftSize;
            fftFreqSpacing = (double)fftSize / sampleRate;

            buffer = new Complex[fftSize];

            SetWindow(FftSharp.Window.Hanning(fftSize));

            (fftIndex1, fftIndex2) = Calculate.FftIndexes(freqMin, freqMax, sampleRate, fftSize);
            fftKeepSize = fftIndex2 - fftIndex1;
            Debug.WriteLine($"Keeping FFT index {fftIndex1} to index {fftIndex2} (from {fftSize / 2} points)");

            pixelValues = new byte[width, fftKeepSize];
            lastFft = new double[fftKeepSize];

        }

        public void SetWindow(double[] window)
        {
            if (window != null)
            {
                if (window.Length == fftSize)
                    this.window = window;
                else
                    throw new ArgumentException("Window length must equal fftSize");
            }
        }

        public void Extend(double[] newData, bool process = true)
        {
            signal.AddRange(newData);
            if (process)
                ProcessAll();
        }

        public void ProcessAll()
        {
            while (signal.Count >= fftSize)
                ProcessNextStep();
        }

        public void ProcessNextStep()
        {
            if (signal.Count < fftSize)
                return;

            // copy the oldest part of the incoming signal into the complex buffer
            for (int i = 0; i < fftSize; i++)
                buffer[i] = new Complex(signal[i] * window[i], 0);

            // crunch the FFT
            FftSharp.Transform.FFT(buffer);

            // calculate PSD magnitude, scale it to Decibels, convert it to pixel intensity
            for (int i = 0; i < fftKeepSize; i++)
            {
                double intensity = buffer[fftIndex1 + i].Magnitude * 2 / fftSize;
                if (dB)
                    intensity = 20 * (float)Math.Log10(intensity + dbMagOffset);
                intensity += pixelOffset;
                intensity *= pixelMult;
                lastFft[i] = intensity;

                intensity = Math.Min(intensity, 255);
                intensity = Math.Max(intensity, 0);
                pixelValues[nextColumnIndex, i] = (byte)intensity;
            }

            // update column indexes
            lastColumnIndex = nextColumnIndex;
            nextColumnIndex += 1;
            if (nextColumnIndex >= pixelValues.GetLength(0))
                nextColumnIndex = 0;
            fftsProcessed += 1;

            // trim the start of the incoming signal by the step size
            if (signal.Count > stepSize)
                signal.RemoveRange(0, stepSize);
        }

        private int fftsProcessedAtLastBitmap = -1;
        public bool isNewImageReady { get { return fftsProcessedAtLastBitmap != fftsProcessed; } }
        public Bitmap GetBitmap()
        {
            fftsProcessedAtLastBitmap = fftsProcessed;
            return Image.Create(pixelValues, cmap, lastColumnIndex);
        }
    }
}
