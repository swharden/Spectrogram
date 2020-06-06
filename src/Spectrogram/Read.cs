using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Spectrogram
{
    public static class Read
    {
        public static double[] WavInt16mono(string filePath, int maxSamples = int.MaxValue, int firstByte = 44)
        {
            if (!File.Exists(filePath))
                throw new ArgumentException($"File does not exist: {filePath}");

            // TODO: read sample rate from WAV header

            byte[] bytes = File.ReadAllBytes(filePath);
            int samplesInFile = (bytes.Length - firstByte) / 2;
            int sampleCount = Math.Min(samplesInFile, maxSamples);

            double[] audio = new double[sampleCount];
            for (int sampleIndex = 0; sampleIndex < audio.Length; sampleIndex++)
                audio[sampleIndex] = BitConverter.ToInt16(bytes, firstByte + sampleIndex * 2);

            return audio;
        }

        public static double[] Div8(double[] input)
        {
            double[] output = new double[input.Length / 8];
            for (int i = 0; i < output.Length; i++)
                output[i] = input[i * 8];
            return output;
        }
    }
}
