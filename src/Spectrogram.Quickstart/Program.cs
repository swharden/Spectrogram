using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;

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

            int fftSize = 4096;
            int stepSize = fftSize / 4;

            int fftCount = (audio.Length - fftSize) / stepSize;
            int[] fftIndexes = new int[fftCount];
            for (int i = 0; i < fftCount; i++)
                fftIndexes[i] = i * fftSize;

            Complex[] buffer = new Complex[fftSize];
            double[] window = FftSharp.Window.Hamming(fftSize);
            List<float[]> psds = new List<float[]>();

            Stopwatch sw = Stopwatch.StartNew();
            for (int fftIndex = 0; fftIndex < fftCount; fftIndex++)
            {
                // load audio into buffer and apply window
                for (int i = 0; i < fftSize; i++)
                    buffer[i] = new Complex(audio[fftIndex * stepSize + i] * window[i], 0);

                // crunch the FFT
                FftSharp.Transform.FFT(buffer);

                // get the scaled magnitude from the positive FFT
                float[] psd = new float[fftSize / 2];
                for (int i = 0; i < buffer.Length / 2; i++)
                    psd[i] = 20 * (float)Math.Log10(buffer[i].Magnitude);

                psds.Add(psd);
            }

            Console.WriteLine($"Processed {psds.Count} FFTs in {sw.ElapsedMilliseconds} ms");
            sw.Restart();

            var cmap = new Colormaps.Viridis();
            Bitmap bmp = Image.Create(psds, cmap, 10);

            Console.WriteLine($"Created spectrogram {bmp.Size} in {sw.ElapsedMilliseconds} ms");
            bmp.Save("spectrogram.png", ImageFormat.Png);
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
    }
}
