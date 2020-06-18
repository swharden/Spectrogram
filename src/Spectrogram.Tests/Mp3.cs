using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spectrogram.Tests
{
    public static class Mp3
    {
        public static double[] Read(string filePath)
        {
            List<double> audio = new List<double>();

            MP3Sharp.MP3Stream stream = new MP3Sharp.MP3Stream(filePath);
            int bufferSize = 4096;
            byte[] buffer = new byte[bufferSize];
            int bytesReturned = 1;
            while (bytesReturned > 0)
            {
                bytesReturned = stream.Read(buffer, 0, buffer.Length);
                for (int i = 0; i < bytesReturned / 2 - 1; i += 2) // TODO: -1 needed? better algo?
                    audio.Add(BitConverter.ToInt16(buffer, i * 2));
            }
            stream.Close();

            return audio.ToArray();
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
