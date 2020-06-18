// Simple WAV file reader by Scott Harden released under a MIT license
// Format here is based on http://soundfile.sapp.org/doc/WaveFormat/

using System;
using System.Diagnostics;
using System.IO;

namespace Spectrogram
{
    public static class WavFile
    {
        private static (string id, uint length) ChunkInfo(BinaryReader br, long position)
        {
            br.BaseStream.Seek(position, SeekOrigin.Begin);
            string chunkID = new string(br.ReadChars(4));
            uint chunkBytes = br.ReadUInt32();
            return (chunkID, chunkBytes);
        }

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
                // The first chunk is RIFF section
                // Length should be the number of bytes in the file minus 4
                var riffChunk = ChunkInfo(br, 0);
                Console.WriteLine($"First chunk '{riffChunk.id}' indicates {riffChunk.length:N0} bytes");
                if (riffChunk.id != "RIFF")
                    throw new InvalidOperationException($"Unsupported WAV format (first chunk ID was '{riffChunk.id}', not 'RIFF')");

                // The second chunk is FORMAT section
                var fmtChunk = ChunkInfo(br, 12);
                Console.WriteLine($"Format chunk '{fmtChunk.id}' indicates {fmtChunk.length:N0} bytes");
                if (fmtChunk.id != "fmt ")
                    throw new InvalidOperationException($"Unsupported WAV format (first chunk ID was '{fmtChunk.id}', not 'fmt ')");
                if (fmtChunk.length != 16)
                    throw new InvalidOperationException($"Unsupported WAV format (expect 16 byte 'fmt' chunk, got {fmtChunk.length} bytes)");

                // By now we verified this is probably a valid FORMAT section, so read its values.
                int audioFormat = br.ReadUInt16();
                Console.WriteLine($"audio format: {audioFormat}");
                if (audioFormat != 1)
                    throw new NotImplementedException("Unsupported WAV format (audio format must be 1, indicating uncompressed PCM data)");

                int channelCount = br.ReadUInt16();
                Console.WriteLine($"channel count: {channelCount}");
                if (channelCount < 0 || channelCount > 2)
                    throw new NotImplementedException($"Unsupported WAV format (must be 1 or 2 channel, file has {channelCount})");

                int sampleRate = (int)br.ReadUInt32();
                Console.WriteLine($"sample rate: {sampleRate} Hz");

                int byteRate = (int)br.ReadUInt32();
                Console.WriteLine($"byteRate: {byteRate}");

                ushort blockSize = br.ReadUInt16();
                Console.WriteLine($"block size: {blockSize} bytes per sample");

                ushort bitsPerSample = br.ReadUInt16();
                Console.WriteLine($"resolution: {bitsPerSample}-bit");
                if (bitsPerSample != 16)
                    throw new NotImplementedException("Only 16-bit WAV files are supported");

                // Cycle custom chunks until we get to the DATA chunk
                // Various chunks may exist until the data chunk appears
                long nextChunkPosition = 36;
                int maximumChunkNumber = 42;
                long firstDataByte = 0;
                long dataByteCount = 0;
                for (int i = 0; i < maximumChunkNumber; i++)
                {
                    var chunk = ChunkInfo(br, nextChunkPosition);
                    Console.WriteLine($"Chunk at {nextChunkPosition} ('{chunk.id}') indicates {chunk.length:N0} bytes");
                    if (chunk.id == "data")
                    {
                        firstDataByte = nextChunkPosition + 8;
                        dataByteCount = chunk.length;
                        break;
                    }
                    nextChunkPosition += chunk.length + 8;
                }
                if (firstDataByte == 0 || dataByteCount == 0)
                    throw new InvalidOperationException("Unsupported WAV format (no 'data' chunk found)");
                Console.WriteLine($"PCM data starts at {firstDataByte} and contains {dataByteCount} bytes");

                // Now read PCM data values into an array and return it
                long sampleCount = dataByteCount / blockSize;
                Debug.WriteLine($"Samples in file: {sampleCount}");

                double[] L = null;
                double[] R = null;

                if (channelCount == 1)
                {
                    L = new double[sampleCount];
                    for (int i = 0; i < sampleCount; i++)
                    {
                        L[i] = br.ReadInt16();
                    }
                }
                else if (channelCount == 2)
                {
                    L = new double[sampleCount];
                    R = new double[sampleCount];
                    for (int i = 0; i < sampleCount; i++)
                    {
                        L[i] = br.ReadInt16();
                        R[i] = br.ReadInt16();
                    }
                }

                return (sampleRate, L, R);
            }
        }
    }
}