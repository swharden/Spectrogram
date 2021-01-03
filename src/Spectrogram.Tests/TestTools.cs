using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spectrogram.Tests
{
    public static class TestTools
    {
        public static (double[] audio, int sampleRate) ReadWavWithNAudio(string filePath)
        {
            using var afr = new NAudio.Wave.AudioFileReader(filePath);
            int sampleRate = afr.WaveFormat.SampleRate;
            int sampleCount = (int)(afr.Length / afr.WaveFormat.BitsPerSample / 8);
            int channelCount = afr.WaveFormat.Channels;
            var audio = new List<double>(sampleCount);
            var buffer = new float[sampleRate * channelCount];
            int samplesRead = 0;
            while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
                audio.AddRange(buffer.Take(samplesRead).Select(x => (double)x));
            return (audio.ToArray(), sampleRate);
        }
    }
}
