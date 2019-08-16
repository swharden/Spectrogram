using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram
{
    public static class Reports
    {
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
    }
}
