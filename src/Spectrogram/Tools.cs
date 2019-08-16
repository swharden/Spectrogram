using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spectrogram
{
    public static class Tools
    {
        public static double[] FFT(double[] values, bool useHammingWindow = true)
        {
            int fftSize = 0;
            for (int i = 4; i < 20; i++)
            {
                int potentialFftSize = (int)Math.Pow(2, i);
                if (potentialFftSize <= values.Length)
                    fftSize = potentialFftSize;
                else
                    break;
            }

            Console.WriteLine($"Processing FFT (size: {fftSize}) of values (size: {values.Length})");

            NAudio.Dsp.Complex[] fft_buffer = new NAudio.Dsp.Complex[fftSize];
            for (int i = 0; i < fftSize; i++)
            {
                fft_buffer[i].X = (float)values[i];
                fft_buffer[i].Y = 0;

                if (useHammingWindow)
                    fft_buffer[i].X *= (float)NAudio.Dsp.FastFourierTransform.HammingWindow(i, fftSize);
            }

            // perform the FFT
            NAudio.Dsp.FastFourierTransform.FFT(true, (int)Math.Log(fftSize, 2.0), fft_buffer);

            // a list with FFT values
            double[] fft = new double[fftSize / 2];

            for (int i = 0; i < fft.Length; i++)
            {
                // should this be sqrt(X^2+Y^2)? log10?

                var fftPointLeft = fft_buffer[i];
                var fftPointRight = fft_buffer[fft_buffer.Length - i - 1];

                fft[i] = 0;
                fft[i] += (double)fftPointLeft.X + (double)fftPointLeft.Y;
                fft[i] += (double)fftPointRight.X + (double)fftPointRight.Y;
                fft[i] /= 2;

                fft[i] = Math.Abs(fft[i]);
            }

            return fft;
        }

        public static void plotValues(double[] values, string saveFilePath = "values.png", int sampleRateHz = 8000)
        {
            var plt = new ScottPlot.Plot();
            plt.PlotSignal(values, sampleRateHz, markerSize: 0, lineWidth: 2);
            plt.Title("Signal");
            plt.YLabel("Value");
            plt.XLabel("Time (sec)");
            plt.AxisAuto(0);
            plt.SaveFig(saveFilePath);
            Console.WriteLine($"Saved: {System.IO.Path.GetFullPath(saveFilePath)}");
        }

        public static void plotFFT(double[] fft, string saveFilePath = "fft.png", int sampleRateHz = 8000)
        {
            var plt = new ScottPlot.Plot();
            double fftSampleRate = (double)fft.Length / sampleRateHz * 2;
            plt.PlotSignal(fft, fftSampleRate, markerSize: 0, lineWidth: 2);
            plt.Title("FFT");
            plt.YLabel("Power");
            plt.XLabel("Frequency (Hz)");
            plt.AxisAuto(0);
            plt.SaveFig(saveFilePath);
            Console.WriteLine($"Saved: {System.IO.Path.GetFullPath(saveFilePath)}");
        }

        public static double[] generateFakeSignal(double durationSeconds = 1, int sampleRateHz = 8000, double signalFrequencyHz = 2000)
        {
            int pointCount = (int)(durationSeconds * sampleRateHz);
            double[] values = new double[pointCount];

            // create a sine wave
            double oscillations = signalFrequencyHz * durationSeconds;
            for (int i = 0; i < values.Length; i++)
                values[i] = Math.Sin(((double)i / values.Length) * 2 * Math.PI * oscillations);

            // add noise
            Random rand = new Random();
            for (int i = 0; i < values.Length; i++)
                values[i] += (rand.NextDouble() - .5) * .1;

            return values;
        }
    }
}
