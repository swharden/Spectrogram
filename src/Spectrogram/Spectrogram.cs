using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram
{
    public class Spectrogram
    {
        public int Width { get { return ffts.Count; } }
        public int Height { get { return settings.Height; } }
        public int FftSize { get { return settings.FftSize; } }
        public double HzPerPx { get { return settings.HzPerPixel; } }
        public double SecPerPx { get { return settings.StepLengthSec; } }
        public int FftsToProcess { get { return (newAudio.Count - settings.FftSize) / settings.StepSize; } }
        public int FftsProcessed { get; private set; }
        public int NextColumnIndex { get { return (FftsProcessed + rollOffset) % Width; } }
        public int OffsetHz { get { return settings.OffsetHz; } set { settings.OffsetHz = value; } }

        private readonly Settings settings;
        public readonly List<double[]> ffts = new List<double[]>(); // TODO: private
        private readonly List<double> newAudio = new List<double>();
        private Colormap cmap = Colormap.Viridis;

        public Spectrogram(int sampleRate, int fftSize, int stepSize,
            double minFreq = 0, double maxFreq = double.PositiveInfinity,
            int? fixedWidth = null, int offsetHz = 0)
        {
            settings = new Settings(sampleRate, fftSize, stepSize, minFreq, maxFreq, offsetHz);

            if (fixedWidth.HasValue)
                SetFixedWidth(fixedWidth.Value);
        }

        public override string ToString()
        {
            double processedSamples = ffts.Count * settings.StepSize + settings.FftSize;
            double processedSec = processedSamples / settings.SampleRate;
            string processedTime = (processedSec < 60) ? $"{processedSec:N2} sec" : $"{processedSec / 60.0:N2} min";

            return $"Spectrogram ({Width}, {Height})" +
                   $"\n  Vertical ({Height} px): " +
                   $"{settings.FreqMin:N0} - {settings.FreqMax:N0} Hz, " +
                   $"FFT size: {settings.FftSize:N0} samples, " +
                   $"{settings.HzPerPixel:N2} Hz/px" +
                   $"\n  Horizontal ({Width} px): " +
                   $"{processedTime}, " +
                   $"window: {settings.FftLengthSec:N2} sec, " +
                   $"step: {settings.StepLengthSec:N2} sec, " +
                   $"overlap: {settings.StepOverlapFrac * 100:N0}%";
        }

        public void SetColormap(Colormap cmap)
        {
            this.cmap = cmap ?? this.cmap;
        }

        public void SetWindow(double[] newWindow)
        {
            if (newWindow.Length > settings.FftSize)
                throw new ArgumentException("window length cannot exceed FFT size");

            for (int i = 0; i < settings.FftSize; i++)
                settings.Window[i] = 0;

            int offset = (settings.FftSize - newWindow.Length) / 2;
            Array.Copy(newWindow, 0, settings.Window, offset, newWindow.Length);
        }

        public void Add(double[] audio, bool process = true)
        {
            newAudio.AddRange(audio);
            if (process)
                Process();
        }

        private int rollOffset = 0;
        public void RollReset(int offset = 0)
        {
            rollOffset = -FftsProcessed + offset;
        }

        public double[][] Process()
        {
            if (FftsToProcess < 1)
                return null;

            int newFftCount = FftsToProcess;
            double[][] newFfts = new double[newFftCount][];

            Parallel.For(0, newFftCount, newFftIndex =>
            {
                FftSharp.Complex[] buffer = new FftSharp.Complex[settings.FftSize];
                int sourceIndex = newFftIndex * settings.StepSize;
                for (int i = 0; i < settings.FftSize; i++)
                    buffer[i].Real = newAudio[sourceIndex + i] * settings.Window[i];

                FftSharp.Transform.FFT(buffer);

                newFfts[newFftIndex] = new double[settings.Height];
                for (int i = 0; i < settings.Height; i++)
                    newFfts[newFftIndex][i] = buffer[settings.FftIndex1 + i].Magnitude / settings.FftSize;
            });

            foreach (var newFft in newFfts)
                ffts.Add(newFft);
            FftsProcessed += newFfts.Length;

            newAudio.RemoveRange(0, newFftCount * settings.StepSize);
            PadOrTrimForFixedWidth();

            return newFfts;
        }

        public Bitmap GetBitmap(double intensity = 1, bool dB = false, bool roll = false) =>
            _GetBitmap(ffts, cmap, intensity, dB, roll, NextColumnIndex);

        public void SaveImage(string fileName, double intensity = 1, bool dB = false, bool roll = false)
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();

            ImageFormat fmt;
            if (extension == ".bmp")
                fmt = ImageFormat.Bmp;
            else if (extension == ".png")
                fmt = ImageFormat.Png;
            else if (extension == ".jpg" || extension == ".jpeg")
                fmt = ImageFormat.Jpeg;
            else if (extension == ".gif")
                fmt = ImageFormat.Gif;
            else
                throw new ArgumentException("unknown file extension");

            _GetBitmap(ffts, cmap, intensity, dB, roll, NextColumnIndex).Save(fileName, fmt);
        }

        public Bitmap GetBitmapMax(double intensity = 1, bool dB = false, bool roll = false, int reduction = 4)
        {
            List<double[]> ffts2 = new List<double[]>();
            for (int i = 0; i < ffts.Count; i++)
            {
                double[] d1 = ffts[i];
                double[] d2 = new double[d1.Length / reduction];
                for (int j = 0; j < d2.Length; j++)
                    for (int k = 0; k < reduction; k++)
                        d2[j] = Math.Max(d2[j], d1[j * reduction + k]);
                ffts2.Add(d2);
            }
            return _GetBitmap(ffts2, cmap, intensity, dB, roll, NextColumnIndex);
        }

        private static Bitmap _GetBitmap(List<double[]> ffts, Colormap cmap, double intensity = 1, bool dB = false, bool roll = false, int rollOffset = 0)
        {
            int Width = ffts.Count;
            int Height = ffts[0].Length;

            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);
            cmap.Apply(bmp);

            var lockRect = new Rectangle(0, 0, Width, Height);
            BitmapData bitmapData = bmp.LockBits(lockRect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            int stride = bitmapData.Stride;

            byte[] bytes = new byte[bitmapData.Stride * bmp.Height];
            Parallel.For(0, Width, col =>
            {
                int sourceCol = col;
                if (roll)
                {
                    sourceCol += Width - rollOffset % Width;
                    if (sourceCol >= Width)
                        sourceCol -= Width;
                }

                for (int row = 0; row < Height; row++)
                {
                    double value = ffts[sourceCol][row];
                    if (dB)
                        value = 20 * Math.Log10(value + 1);
                    value *= intensity;
                    value = Math.Min(value, 255);
                    int bytePosition = (Height - 1 - row) * stride + col;
                    bytes[bytePosition] = (byte)value;
                }
            });

            Marshal.Copy(bytes, 0, bitmapData.Scan0, bytes.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }

        /// <summary>
        /// Export spectrogram data as a SFF (spectrogram file format) binary file
        /// </summary>
        public void SaveData(string filePath, bool complexFormat = false)
        {
            if (!filePath.EndsWith(".sff", StringComparison.OrdinalIgnoreCase))
                filePath += ".sff";

            const byte FileFormatVersionMajor = 1;
            const byte FileFormatVersionMinor = 1;

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
            string fileInfo = $"Spectrogram File Format {FileFormatVersionMajor}.{FileFormatVersionMinor}\r\n";
            byte[] fileInfoBytes = Encoding.UTF8.GetBytes(fileInfo);
            if (fileInfoBytes.Length > 32)
                throw new InvalidDataException("file info cannot exceed 32 bytes");
            Array.Copy(fileInfoBytes, 0, header, 8, fileInfoBytes.Length);

            // version
            header[40] = FileFormatVersionMajor;
            header[41] = FileFormatVersionMinor;

            // time information
            Array.Copy(BitConverter.GetBytes(settings.SampleRate), 0, header, 42, 4);
            Array.Copy(BitConverter.GetBytes(settings.StepSize), 0, header, 46, 4);
            Array.Copy(BitConverter.GetBytes(Width), 0, header, 50, 4);

            // frequency information
            Array.Copy(BitConverter.GetBytes(FftSize), 0, header, 54, 4);
            Array.Copy(BitConverter.GetBytes(settings.FftIndex1), 0, header, 58, 4);
            Array.Copy(BitConverter.GetBytes(Height), 0, header, 62, 4);
            Array.Copy(BitConverter.GetBytes(OffsetHz), 0, header, 66, 4);

            // data encoding details
            byte valuesPerPoint = 1; // only 2 for complex data
            byte bytesPerValue = 8; // a double is 8 bytes
            byte decibelUnits = 0; // 1 if units are in dB
            byte dataExtraByte = 0; // unused
            header[70] = valuesPerPoint;
            header[71] = bytesPerValue;
            header[72] = decibelUnits;
            header[73] = dataExtraByte;

            // source file date and time
            int twoDigitYear = DateTime.UtcNow.Year - 2000;
            header[74] = (byte)twoDigitYear;
            header[75] = (byte)DateTime.UtcNow.Month;
            header[76] = (byte)DateTime.UtcNow.Day;
            header[77] = (byte)DateTime.UtcNow.Hour;
            header[78] = (byte)DateTime.UtcNow.Minute;
            header[79] = (byte)DateTime.UtcNow.Second;

            // binary data location
            int firstDataByte = header.Length;
            Array.Copy(BitConverter.GetBytes(firstDataByte), 0, header, 80, 4);

            // create bytes to write to file
            int dataPointCount = Height * Width;
            int bytesPerPoint = bytesPerValue * valuesPerPoint;
            byte[] fileBytes = new byte[header.Length + dataPointCount * bytesPerPoint];
            Array.Copy(header, 0, fileBytes, 0, header.Length);

            // copy data into byte area
            int bytesPerColumn = Height * bytesPerPoint;
            for (int x = 0; x < Width; x++)
            {
                int columnOffset = bytesPerColumn * x;
                for (int y = 0; y < Height; y++)
                {
                    int rowOffset = y * bytesPerPoint;
                    int valueOffset = firstDataByte + columnOffset + rowOffset;
                    Array.Copy(BitConverter.GetBytes(ffts[x][y]), 0, fileBytes, valueOffset, 8);
                }
            }

            // write file to disk
            File.WriteAllBytes(filePath, fileBytes);
        }

        private int fixedWidth = 0;
        public void SetFixedWidth(int width)
        {
            fixedWidth = width;
            PadOrTrimForFixedWidth();
        }

        private void PadOrTrimForFixedWidth()
        {
            if (fixedWidth > 0)
            {
                int overhang = Width - fixedWidth;
                if (overhang > 0)
                    ffts.RemoveRange(0, overhang);

                while (ffts.Count < fixedWidth)
                    ffts.Insert(0, new double[Height]);
            }
        }

        public Bitmap GetVerticalScale(int width, int offsetHz = 0, int tickSize = 3, int reduction = 1)
        {
            return Scale.Vertical(width, settings, offsetHz, tickSize, reduction);
        }

        public int PixelY(double frequency, int reduction = 1)
        {
            // TODO: tick generation should use this method
            int pixelsFromZeroHz = (int)(settings.PxPerHz * frequency / reduction);
            int pixelsFromMinFreq = pixelsFromZeroHz - settings.FftIndex1 / reduction + 1;
            int pixelRow = settings.Height / reduction - 1 - pixelsFromMinFreq;
            return pixelRow - 1;
        }
    }
}
