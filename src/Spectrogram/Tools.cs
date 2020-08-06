using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrogram
{
    public static class Tools
    {
        /// <summary>
        /// Collapse the 2D spectrogram into a 1D array (mean power of each frequency)
        /// </summary>
        public static double[] SffMeanFFT(SFF sff, bool dB = false)
        {
            double[] mean = new double[sff.Ffts[0].Length];

            foreach (var fft in sff.Ffts)
                for (int y = 0; y < fft.Length; y++)
                    mean[y] += fft[y];

            for (int i = 0; i < mean.Length; i++)
                mean[i] /= sff.Ffts.Count();

            if (dB)
                for (int i = 0; i < mean.Length; i++)
                    mean[i] = 20 * Math.Log10(mean[i]);

            if (mean[mean.Length - 1] <= 0)
                mean[mean.Length - 1] = mean[mean.Length - 2];

            return mean;
        }

        /// <summary>
        /// Collapse the 2D spectrogram into a 1D array (mean power of each time point)
        /// </summary>
        public static double[] SffMeanPower(SFF sff, bool dB = false)
        {
            double[] power = new double[sff.Ffts.Count];

            for (int i = 0; i < sff.Ffts.Count; i++)
                power[i] = (double)sff.Ffts[i].Sum() / sff.Ffts[i].Length;

            if (dB)
                for (int i = 0; i < power.Length; i++)
                    power[i] = 20 * Math.Log10(power[i]);

            return power;
        }

        public static double GetPeakFrequency(SFF sff, bool firstFftOnly = false)
        {
            double[] freqs = firstFftOnly ? sff.Ffts[0] : SffMeanFFT(sff, false);

            int peakIndex = 0;
            double peakPower = 0;
            for (int i = 0; i < freqs.Length; i++)
            {
                if (freqs[i] > peakPower)
                {
                    peakPower = freqs[i];
                    peakIndex = i;
                }
            }

            double maxFreq = sff.SampleRate / 2;
            double frac = peakIndex / (double)sff.ImageHeight;

            if (sff.MelBinCount > 0)
            {
                double maxMel = FftSharp.Transform.MelFromFreq(maxFreq);
                return FftSharp.Transform.MelToFreq(frac * maxMel);
            }
            else
            {
                return frac * maxFreq;
            }
        }

        public static int GetPianoKey(double frequencyHz)
        {
            double pianoKey = (39.86 * Math.Log10(frequencyHz / 440)) + 49;
            return (int)Math.Round(pianoKey);
        }

        public static int GetMidiNote(double frequencyHz)
        {
            return GetPianoKey(frequencyHz) + 20;
        }
    }
}
