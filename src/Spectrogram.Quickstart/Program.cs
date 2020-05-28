using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;

namespace Spectrogram.Quickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Saving output in: {System.IO.Path.GetFullPath("./")}");
            string audioFilePath = "../../../../../data/Handel - Air and Variations.mp3";
            //string audioFilePath = "../../../../../data/cant-do-that.mp3";

            (double[] audio, int sampleRate) = Read.MP3(audioFilePath);

            PlotPCM(audio, sampleRate);

            int fftSize = 4096 * 4;
            int stepSize = fftSize / 8;

            int fftCount = (audio.Length - fftSize) / stepSize;
            int[] fftIndexes = new int[fftCount];
            for (int i = 0; i < fftCount; i++)
                fftIndexes[i] = i * fftSize;

            Complex[] buffer = new Complex[fftSize];
            double[] window = FftSharp.Window.Hanning(fftSize);
            List<float[]> psds = new List<float[]>();

            // only save a range of the FFT
            int fftKeepIndexStart = 0;
            int fftKeepIndexEnd = fftSize / 2;
            fftKeepIndexEnd /= 10;
            int fftKeepSize = fftKeepIndexEnd - fftKeepIndexStart;

            Stopwatch sw = Stopwatch.StartNew();
            for (int fftIndex = 0; fftIndex < fftCount; fftIndex++)
            {
                // load audio into buffer and apply window
                for (int i = 0; i < fftSize; i++)
                    buffer[i] = new Complex(audio[fftIndex * stepSize + i] * window[i], 0);

                // crunch the FFT
                FftSharp.Transform.FFT(buffer);

                // collect the magnitude of the positive FFT
                float[] psd = new float[fftKeepSize];
                for (int i = fftKeepIndexStart; i < fftKeepIndexEnd; i++)
                    psd[i] = 20 * (float)Math.Log10(buffer[i].Magnitude + 1); // note: plus one here

                // store it to make the spectrogram
                psds.Add(psd);
            }

            Console.WriteLine($"Processed {psds.Count} FFTs in {sw.ElapsedMilliseconds} ms");
            (float min, float max) = Stats(psds);
            sw.Restart();

            var cmap = new Colormaps.Viridis();
            Bitmap bmp = Image.Create(psds, cmap, 256 / (max - min), min);

            Console.WriteLine($"Created spectrogram {bmp.Size} in {sw.ElapsedMilliseconds} ms");
            bmp.Save("spectrogram.png", ImageFormat.Png);
            bmp.Save("../../../../../dev/spectrogram-song.jpg", ImageFormat.Png);
            //bmp.Save("spectrogram.jpg", ImageFormat.Png);
        }

        static (float min, float max) Stats(List<float[]> psds)
        {
            int width = psds.Count;
            int height = psds[0].Length;
            int count = psds[0].Length * psds.Count;

            Console.WriteLine($"Analyzing {count} values...");

            float[] allValues = new float[count];
            for (int col = 0; col < width; col++)
            {
                for (int row = 0; row < height; row++)
                {
                    allValues[col * height + row] = psds[col][row];
                }
            }

            Array.Sort(allValues);
            float min = allValues[0];
            float max = allValues[count - 1];

            Console.WriteLine($"Width: {width}");
            Console.WriteLine($"Height: {height}");
            Console.WriteLine($"Values: {count}");
            Console.WriteLine();
            Console.WriteLine($"Min: {min}");
            Console.WriteLine($"Max: {max}");

            double[] percentiles = new double[101];
            percentiles[0] = allValues[0];
            for (int i = 1; i < 100; i++)
                percentiles[i] = allValues[(int)(allValues.Length * i / 100.0)];
            percentiles[100] = allValues[allValues.Length - 1];

            var plt = new ScottPlot.Plot(600, 400);
            plt.PlotSignal(percentiles);
            plt.Title("Power Spectral Density Distribution");
            plt.XLabel("Percentile");
            plt.YLabel("Value");
            plt.Axis(y2: 260);
            plt.SaveFig("percentiles.png");

            //return (min, max);
            return (min, (float)percentiles[99]);
        }

        static void PlotPCM(double[] audio, int sampleRate)
        {
            var plt = new ScottPlot.Plot(600, 400);
            plt.PlotSignal(audio, sampleRate);
            //plt.Title("PCM");
            plt.XLabel("Time (seconds)");
            plt.YLabel("Amplitude");
            plt.AxisAuto(0);
            plt.SaveFig("pcm.png");
        }

        static void PlotPSDs(List<float[]> psds, double sampleFrequency)
        {
            var plt = new ScottPlot.Plot(600, 400);
            for (int i = 0; i < psds.Count; i += 50)
            {
                double[] values = psds[i].Select(x => (double)x).ToArray();
                plt.PlotSignal(values, sampleFrequency);
            }
            //plt.Title("PSDs");
            plt.XLabel("Frequency (Hz)");
            plt.YLabel("Power (dB)");
            plt.AxisAuto(0);
            plt.SaveFig("psd.png");
        }

        static void PlotLogCurves()
        {
            var plt = new ScottPlot.Plot(600, 400);
            plt.PlotFunction(new Func<double, double?>(x => Math.Log10(x)), label: "Log10");
            plt.PlotFunction(new Func<double, double?>(x => Math.Sqrt(x)), label: "Sqrt");
            plt.Axis(0, 10, -2, 5);
            plt.Legend();
            plt.SaveFig("log.png");
        }
    }
}
