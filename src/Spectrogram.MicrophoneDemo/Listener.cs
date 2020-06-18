using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram.MicrophoneDemo
{
    public class Listener : IDisposable
    {
        public readonly int SampleRate;
        private readonly NAudio.Wave.WaveInEvent wvin;
        public double AmplitudeFrac { get; private set; }
        public double TotalSamples { get; private set; }
        public double TotalTimeSec { get { return (double)TotalSamples / SampleRate; } }
        private readonly List<double> audio = new List<double>();
        public int SamplesInMemory { get { return audio.Count; } }

        public Listener(int deviceIndex, int sampleRate)
        {
            SampleRate = sampleRate;
            wvin = new NAudio.Wave.WaveInEvent
            {
                DeviceNumber = deviceIndex,
                WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bits: 16, channels: 1),
                BufferMilliseconds = 20
            };
            wvin.DataAvailable += OnNewAudioData;
            wvin.StartRecording();
        }

        public void Dispose()
        {
            wvin?.StopRecording();
            wvin?.Dispose();
        }

        private void OnNewAudioData(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int newSampleCount = args.BytesRecorded / bytesPerSample;
            double[] buffer = new double[newSampleCount];
            double peak = 0;
            for (int i = 0; i < newSampleCount; i++)
            {
                buffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
                peak = Math.Max(peak, buffer[i]);
            }
            lock (audio)
            {
                audio.AddRange(buffer);
            }
            AmplitudeFrac = peak / (1 << 15);
            TotalSamples += newSampleCount;
        }

        public double[] GetNewAudio()
        {
            lock (audio)
            {
                double[] values = new double[audio.Count];
                for (int i = 0; i < values.Length; i++)
                    values[i] = audio[i];
                audio.RemoveRange(0, values.Length);
                return values;
            }
        }
    }
}
