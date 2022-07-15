using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram;

/// <summary>
/// This class creates spectrogram images from fixed-length array containing signal data.
/// </summary>
public class StaticSpectrogram
{
    private double[,] Ffts; // [columns, frequencies]
    private readonly double[] Signal;

    public int StepSize = 200;
    public int FftSize = 1 << 12;

    public FftSharp.Window Window = new FftSharp.Windows.Hanning();
    public IColormap Colormap = new Colormaps.Viridis();

    public StaticSpectrogram(double[] signal)
    {
        Signal = signal;
        Recalculate();
    }

    public void Recalculate()
    {
        int columns = (Signal.Length - FftSize) / StepSize;
        Ffts = new double[columns, FftSize / 2];

        double[] buffer = new double[FftSize];
        for (int i = 0; i < columns; i++)
        {
            Array.Copy(Signal, i * StepSize, buffer, 0, FftSize);

            Window.ApplyInPlace(buffer);
            double[] fft = FftSharp.Transform.FFTmagnitude(buffer);

            for (int j = 0; j < FftSize / 2; j++)
            {
                Ffts[i, j] = fft[j];
            }
        }
    }

    public void SaveImage(string filePath, double mult = 1)
    {
        filePath = System.IO.Path.GetFullPath(filePath);

        int newBottomIndex = 0;
        int newTopIndex = 500;
        int newHeightCount = newTopIndex - newBottomIndex;
        double[,] ffts2 = new double[Ffts.GetLength(0), newHeightCount];
        for (int i = 0; i < Ffts.GetLength(0); i++)
        {
            for (int j = 0; j < newHeightCount; j++)
            {
                ffts2[i, j] = Ffts[i, j];
            }
        }

        Tools.FftsToImage(ffts2, mult, Colormap).Save(filePath);
        Console.WriteLine($"Saved: {filePath}");
    }
}
