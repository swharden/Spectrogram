using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram.Settings
{
    public class FftSettings
    {
        // The Spectrograph library does two things:
        //   1) convert a signal to a FFT List
        //   2) convert a FFT list to a Bitmap

        // This class stores settings that control how the FFT list is created (#2)

        public readonly int sampleRate;
        public readonly int fftSize; // todo: change this to fftInputPointCount
        public int segmentSize;

        public FftSettings(int sampleRate, int fftSize, int segmentSize)
        {
            if (sampleRate <= 0)
                throw new ArgumentException("Sample rate must be greater than 0");

            if (!Operations.IsPowerOfTwo(fftSize))
                throw new ArgumentException("fftSize must be a power of 2");

            this.sampleRate = sampleRate;
            this.fftSize = fftSize;
            this.segmentSize = segmentSize;
        }

        public double maxFreq { get { return sampleRate / 2; } }
        public int fftOutputPointCount { get { return fftSize / 2; } }
        public double fftResolution { get { return maxFreq / fftOutputPointCount; } }
        public double segmentsPerSecond { get { return sampleRate / segmentSize; } }

        public override string ToString()
        {
            string msg = "";
            msg += $"Sample rate: {sampleRate} Hz\n";
            msg += $"Maximum visible Frequency: {maxFreq} Hz\n";
            msg += $"FFT Size: {fftOutputPointCount} points\n";
            msg += $"FFT Resolution: {fftResolution} Hz\n";
            return msg.Trim();
        }

        public int IndexFromFrequency(double frequency)
        {
            return (int)(frequency / fftResolution);
        }

        public double FrequencyFromIndex(int index)
        {
            return index * fftResolution;
        }
    }
}
