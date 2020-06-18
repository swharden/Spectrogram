// Simple WAV file reader by Scott Harden released under a MIT license
// Format here is based on http://soundfile.sapp.org/doc/WaveFormat/

using System;
using System.Diagnostics;
using System.IO;

namespace Spectrogram
{
    public static class WavFile
    {
        public static (int sampleRate, double[] L) ReadMono(string filePath)
        {
            (int sampleRate, double[] L, _) = ReadStereo(filePath);
            return (sampleRate, L);
        }

        public static (int sampleRate, double[] L, double[] R) ReadStereo(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs))
            {
                // read and verify content of the header
                if (new string(br.ReadChars(4)) != "RIFF")
                    throw new ArgumentException("invalid WAV header (no RIFF)");

                uint chunkSize = br.ReadUInt32();
                if (new string(br.ReadChars(4)) != "WAVE")
                    throw new ArgumentException("invalid WAV file (expected 'WAVE' at byte 8)");

                string formatString = new string(br.ReadChars(4));
                if (formatString != "fmt ")
                    throw new NotImplementedException("unsupported WAV header (expected 'fmt ' at byte 12)");

                int chunkOneSize = (int)br.ReadUInt32();
                int firstByteAfterChunk1 = (int)br.BaseStream.Position + chunkOneSize;
                if (chunkOneSize != 16)
                    throw new NotImplementedException("unsupported WAV header (chunk 1 must be 16 bytes in length)");

                int audioFormat = br.ReadUInt16();
                Debug.WriteLine($"audio format: {audioFormat}");
                if (audioFormat != 1)
                    throw new NotImplementedException("unsupported WAV header (audio format must be 1, indicating uncompressed PCM data)");

                int channelCount = br.ReadUInt16();
                Debug.WriteLine($"channel count: {channelCount}");

                int sampleRate = (int)br.ReadUInt32();
                Debug.WriteLine($"sample rate: {sampleRate} Hz");

                int byteRate = (int)br.ReadUInt32();
                Debug.WriteLine($"byteRate: {byteRate}");

                ushort blockSize = br.ReadUInt16();
                Debug.WriteLine($"block size: {blockSize} bytes per sample");

                ushort bitsPerSample = br.ReadUInt16();
                Debug.WriteLine($"resolution: {bitsPerSample}-bit");
                if (bitsPerSample != 16)
                    throw new NotImplementedException("Only 16-bit WAV files are supported");

                string dataChars = new string(br.ReadChars(4));
                Debug.WriteLine($"Data characters: {dataChars}");

                // this may be the number of data bytes, but don't rely on it to be.
                int finalNumber = (int)br.ReadUInt32();
                Debug.WriteLine($"Final UInt32: {finalNumber}");

                int bytesRemaining = (int)(fs.Length - br.BaseStream.Position);
                Debug.WriteLine($"Bytes remaining: {bytesRemaining}");
                int sampleCount = bytesRemaining / blockSize;
                Debug.WriteLine($"Samples in file: {sampleCount}");
                int timePoints = sampleCount / channelCount;
                Debug.WriteLine($"Time points in file: {timePoints}");
                Debug.WriteLine($"First data byte: {br.BaseStream.Position}");

                if (channelCount == 1)
                {
                    double[] L = new double[timePoints];
                    for (int i = 0; i < timePoints; i++)
                    {
                        L[i] = br.ReadInt16();
                    }
                    return (sampleRate, L, null);
                }
                else if (channelCount == 2)
                {
                    double[] L = new double[timePoints];
                    double[] R = new double[timePoints];
                    for (int i = 0; i < timePoints; i++)
                    {
                        L[i] = br.ReadInt16();
                        R[i] = br.ReadInt16();
                    }
                    return (sampleRate, L, R);
                }
                else
                {
                    throw new InvalidOperationException("channel must be 1 or 2");
                }
            }
        }
    }
}