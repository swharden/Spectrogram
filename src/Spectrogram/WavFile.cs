// Simple WAV file reader by Scott Harden released under a MIT license
// Format here is based on http://soundfile.sapp.org/doc/WaveFormat/

using System;
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
                if (audioFormat != 1)
                    throw new NotImplementedException("unsupported WAV header (audio format must be 1, indicating uncompressed PCM data)");

                int channelCount = br.ReadUInt16();
                int sampleRate = (int)br.ReadUInt32();
                int byteRate = (int)br.ReadUInt32();
                ushort blockSize = br.ReadUInt16();
                ushort bitsPerSample = br.ReadUInt16();
                if (bitsPerSample != 16)
                    throw new NotImplementedException("Only 16-bit WAV files are supported");

                if (new string(br.ReadChars(4)) != "data")
                    throw new ArgumentException($"invalid WAV file (expected 'data' at byte {br.BaseStream.Position - 4})");

                // read data values from the WAV file
                int dataByteCount = (int)br.ReadUInt32();
                int timePoints = dataByteCount / blockSize;
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