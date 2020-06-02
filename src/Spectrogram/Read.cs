using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spectrogram
{
    public static class Read
    {
        // TODO: remove this dependency
        public static (double[] audio, int sampleRate) WavInt16mono(string filePath)
        {
            filePath = System.IO.Path.GetFullPath(filePath);
            if (!System.IO.File.Exists(filePath))
                throw new ArgumentException($"File does not exist: {filePath}");

            Console.WriteLine($"Decoding {System.IO.Path.GetFileName(filePath)}...");
            Stopwatch sw = Stopwatch.StartNew();

            byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            int firstByte = 44;

            // TODO: read sample rate from header
            int sampleRate = 44_100;

            double[] audio = new double[(bytes.Length - firstByte) / 2];

            for (int i = 0; i < audio.Length; i++)
                audio[i] = BitConverter.ToInt16(bytes, firstByte + i * 2);

            Debug.WriteLine($"Decoded {audio.Length} audio values " +
                $"({(double)audio.Length / sampleRate:N2} sec) " +
                $"in {sw.ElapsedMilliseconds:N0} ms.");

            return (audio, sampleRate);
        }

        // TODO: true WAV reader
        public static (double[] audio, int sampleRate) MP3(string filePath)
        {
            filePath = System.IO.Path.GetFullPath(filePath);
            if (!System.IO.File.Exists(filePath))
                throw new ArgumentException($"File does not exist: {filePath}");

            Debug.WriteLine($"Decoding '{System.IO.Path.GetFileName(filePath)}'...");
            Stopwatch sw = Stopwatch.StartNew();
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

            // TODO: read sample rate from header
            int sampleRate = 44_100;
            double durationSec = (double)audio.Count / sampleRate;

            Debug.WriteLine($"Decoded {audio.Count} audio values ({durationSec:N2} sec) in {sw.ElapsedMilliseconds:N0} ms.");
            return (audio.ToArray(), sampleRate);
        }
    }
}
