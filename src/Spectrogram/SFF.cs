using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Spectrogram
{
    // Spectrogram File Format reader/writer
    public class SFF
    {
        public readonly byte VersionMajor = 1;
        public readonly byte VersionMinor = 1;

        // time information
        public int SampleRate { get; private set; }
        public int StepSize { get; private set; }
        public int Width { get; private set; }

        // frequency information
        public int FftSize { get; private set; }
        public int FftFirstIndex { get; private set; }
        public int Height { get; private set; }
        public int OffsetHz { get; private set; }
        public int MelBinCount { get; private set; }

        public List<double[]> FFTs { get; private set; }

        public SFF()
        {

        }

        public SFF(string loadFilePath)
        {
            Load(loadFilePath);
        }

        public SFF(Spectrogram spec, int melBinCount = 0)
        {
            SampleRate = spec.SampleRate;
            StepSize = spec.StepSize;
            Width = spec.Width;
            FftSize = spec.FftSize;
            FftFirstIndex = spec.NextColumnIndex;
            Height = spec.Height;
            OffsetHz = spec.OffsetHz;
            MelBinCount = melBinCount;
            if (MelBinCount > 0)
                FFTs = spec.GetMelFFTs(melBinCount);
            else
                FFTs = spec.GetFFTs();
        }

        public void Load(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);

            // ensure the first 4 bytes match what we expect
            int magicNumber = BitConverter.ToInt32(bytes, 0);
            if (magicNumber != 1179014099)
                throw new InvalidDataException("not a valid SFF file");

            // read file version
            byte versionMajor = bytes[40];
            byte versionMinor = bytes[41];
            Console.WriteLine($"SFF version {versionMajor}.{versionMinor}");

            // read time information
            SampleRate = BitConverter.ToInt32(bytes, 42);
            StepSize = BitConverter.ToInt32(bytes, 46);
            Width = BitConverter.ToInt32(bytes, 50);

            // read frequency information
            FftSize = BitConverter.ToInt32(bytes, 54);
            FftFirstIndex = BitConverter.ToInt32(bytes, 58);
            Height = BitConverter.ToInt32(bytes, 62);
            OffsetHz = BitConverter.ToInt32(bytes, 66);
            MelBinCount = BitConverter.ToInt32(bytes, 84);

            // data format
            byte valuesPerPoint = bytes[70];
            bool isComplex = valuesPerPoint == 2;
            byte bytesPerValue = bytes[71];
            bool decibels = bytes[72] == 1;

            // recording start time
            DateTime dt = new DateTime(bytes[74] + 2000, bytes[75], bytes[76], bytes[77], bytes[78], bytes[79]);

            // data storage
            UInt32 firstDataByte = BitConverter.ToUInt32(bytes, 80);
        }

        public void Save(string filePath)
        {
            byte[] header = new byte[256];

            // file type designator
            header[0] = 211; // intentionally non-ASCII
            header[1] = (byte)'S';
            header[2] = (byte)'F';
            header[3] = (byte)'F';
            header[4] = (byte)'\r';
            header[5] = (byte)'\n';
            header[6] = (byte)' ';
            header[7] = (byte)'\n';

            int magicNumber = BitConverter.ToInt32(header, 0);
            if (magicNumber != 1179014099)
                throw new InvalidDataException("magic number for SFF files is 1179014099");

            // plain text helpful for people who open this file in a text editor
            string fileInfo = $"Spectrogram File Format {VersionMajor}.{VersionMinor}\r\n";
            byte[] fileInfoBytes = Encoding.UTF8.GetBytes(fileInfo);
            if (fileInfoBytes.Length > 32)
                throw new InvalidDataException("file info cannot exceed 32 bytes");
            Array.Copy(fileInfoBytes, 0, header, 8, fileInfoBytes.Length);

            // version
            header[40] = VersionMajor;
            header[41] = VersionMinor;

            // time information
            Array.Copy(BitConverter.GetBytes(SampleRate), 0, header, 42, 4);
            Array.Copy(BitConverter.GetBytes(StepSize), 0, header, 46, 4);
            Array.Copy(BitConverter.GetBytes(Width), 0, header, 50, 4);

            // frequency information
            Array.Copy(BitConverter.GetBytes(FftSize), 0, header, 54, 4);
            Array.Copy(BitConverter.GetBytes(FftFirstIndex), 0, header, 58, 4);
            Array.Copy(BitConverter.GetBytes(Height), 0, header, 62, 4);
            Array.Copy(BitConverter.GetBytes(OffsetHz), 0, header, 66, 4);

            // data encoding details
            byte valuesPerPoint = 2; // 1 for magnitude or power data, 2 for complex data
            byte bytesPerValue = 8; // a double is 8 bytes
            byte decibelUnits = 0; // 1 if units are in dB
            byte dataExtraByte = 0; // unused
            header[70] = valuesPerPoint;
            header[71] = bytesPerValue;
            header[72] = decibelUnits;
            header[73] = dataExtraByte;

            // source file date and time
            header[74] = (byte)(DateTime.UtcNow.Year - 2000); // 2-digit year
            header[75] = (byte)DateTime.UtcNow.Month;
            header[76] = (byte)DateTime.UtcNow.Day;
            header[77] = (byte)DateTime.UtcNow.Hour;
            header[78] = (byte)DateTime.UtcNow.Minute;
            header[79] = (byte)DateTime.UtcNow.Second;

            // ADD NEW VALUES HERE (after byte 80)
            Array.Copy(BitConverter.GetBytes(MelBinCount), 0, header, 84, 4);

            // binary data location (keep this at byte 80)
            int firstDataByte = header.Length;
            Array.Copy(BitConverter.GetBytes(firstDataByte), 0, header, 80, 4);

            // create bytes to write to file
            int imageHeight = FFTs[0].Length;
            int imageWidth = FFTs.Count;
            int dataPointCount = imageHeight * imageWidth;
            int bytesPerPoint = bytesPerValue * valuesPerPoint;
            byte[] fileBytes = new byte[header.Length + dataPointCount * bytesPerPoint];
            Array.Copy(header, 0, fileBytes, 0, header.Length);

            // copy data into byte area
            int bytesPerColumn = imageHeight * bytesPerPoint;
            for (int x = 0; x < imageWidth; x++)
            {
                int columnOffset = bytesPerColumn * x;
                for (int y = 0; y < imageHeight; y++)
                {
                    int rowOffset = y * bytesPerPoint;
                    int valueOffset = firstDataByte + columnOffset + rowOffset;
                    double value = FFTs[x][y];
                    Array.Copy(BitConverter.GetBytes(value), 0, fileBytes, valueOffset, 8);
                }
            }

            // write file to disk
            File.WriteAllBytes(filePath, fileBytes);
        }
    }
}
