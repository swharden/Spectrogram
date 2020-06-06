using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram
{
    public class Settings
    {
        public readonly int SampleRate;

        // vertical information
        public readonly int FftSize;
        public readonly double FreqNyquist;
        public readonly double HzPerPixel;
        public readonly int FftIndex1;
        public readonly int FftIndex2;
        public readonly double FreqMin;
        public readonly double FreqMax;
        public readonly int Height;

        // horizontal information
        public readonly int WindowSize;
        public readonly double WindowLengthSec;
        public readonly double[] Window;
        public readonly int StepSize;
        public readonly double StepLengthSec;
        public readonly double StepOverlapFrac;
        public readonly double StepOverlapSec;

        public Settings(int sampleRate, int fftSize, int windowSize, int stepSize,
            double minFreq = 0, double maxFreq = double.PositiveInfinity)
        {
            if (FftSharp.Transform.IsPowerOfTwo(fftSize) == false)
                throw new ArgumentException("FFT size must be a power of 2");

            if (FftSharp.Transform.IsPowerOfTwo(windowSize) == false)
                throw new ArgumentException("window size must be a power of 2");

            // FFT info
            SampleRate = sampleRate;
            FftSize = fftSize;
            WindowSize = windowSize;
            WindowLengthSec = (double)WindowSize / SampleRate;
            StepSize = stepSize;

            // vertical
            FreqNyquist = sampleRate / 2;
            HzPerPixel = (double)sampleRate / fftSize;
            FftIndex1 = (minFreq == 0) ? 0 : (int)(minFreq / HzPerPixel);
            FftIndex2 = (maxFreq > fftSize / 2) ? fftSize / 2 : (int)(maxFreq / HzPerPixel);
            Height = FftIndex2 - FftIndex1;
            FreqMin = FftIndex1 * HzPerPixel;
            FreqMax = FftIndex2 * HzPerPixel;

            // horizontal
            StepLengthSec = (double)StepSize / sampleRate;
            Window = FftSharp.Window.Hanning(WindowSize);
            StepOverlapSec = WindowLengthSec - StepLengthSec;
            StepOverlapFrac = StepOverlapSec / WindowLengthSec;
        }
    }
}
