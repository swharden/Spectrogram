using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram
{
    public static class Calculate
    {
        public static (int index1, int index2) FftIndexes(double freqMin, double freqMax, int sampleRate, int fftSize)
        {
            double[] freqs = FftSharp.Transform.FFTfreq(sampleRate, fftSize / 2);

            int index2 = fftSize / 2;
            for (int i = 0; i < fftSize / 2; i++)
                if (freqMax >= freqs[i])
                    index2 = i;

            int index1 = 0;
            for (int i = fftSize / 2 - 1; i >= 0; i--)
                if (freqMin <= freqs[i])
                    index1 = i;

            return (index1, index2);
        }
    }
}
