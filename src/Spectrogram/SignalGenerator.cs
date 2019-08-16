using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram
{
    public static class SignalGenerator
    {
        public static float[] NoisySin(
            double durationSeconds = 1,
            int sampleRateHz = 8000,
            double signalFrequencyHz = 2000,
            double noiseLevel = .1
            )
        {
            int pointCount = (int)(durationSeconds * sampleRateHz);
            float[] values = new float[pointCount];

            // create a sine wave
            float oscillations = (float)(signalFrequencyHz * durationSeconds);
            for (int i = 0; i < values.Length; i++)
                values[i] = (float)Math.Sin(((float)i / values.Length) * 2 * Math.PI * oscillations);

            // add noise
            Random rand = new Random();
            for (int i = 0; i < values.Length; i++)
                values[i] += (float)((rand.NextDouble() - .5) * noiseLevel);

            return values;
        }
    }
}
