using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrogram.Tests
{
    public static class AudioFile
    {
        public static (double[] audio, int sampleRate) ReadWAV(string filePath, double multiplier = 16_000)
        {
            using var afr = new NAudio.Wave.AudioFileReader(filePath);
            int sampleRate = afr.WaveFormat.SampleRate;
            int sampleCount = (int)(afr.Length / afr.WaveFormat.BitsPerSample / 8);
            int channelCount = afr.WaveFormat.Channels;
            var audio = new List<double>(sampleCount);
            var buffer = new float[sampleRate * channelCount];
            int samplesRead = 0;
            while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
                audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));
            return (audio.ToArray(), sampleRate);
        }

        public static double[] ReadMP3(string filePath, int bufferSize = 4096)
        {
            List<double> audio = new List<double>();
            MP3Sharp.MP3Stream stream = new MP3Sharp.MP3Stream(filePath);
            byte[] buffer = new byte[bufferSize];
            int bytesReturned = 1;
            while (bytesReturned > 0)
            {
                bytesReturned = stream.Read(buffer, 0, bufferSize);
                for (int i = 0; i < bytesReturned / 2 - 1; i += 2)
                    audio.Add(BitConverter.ToInt16(buffer, i * 2));
            }
            stream.Close();
            return audio.ToArray();
        }
    }
}
