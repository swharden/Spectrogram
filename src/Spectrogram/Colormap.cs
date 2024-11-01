using System;
using System.Linq;
using SkiaSharp;

namespace Spectrogram;

public class Colormap(ScottPlot.IColormap colormap)
{
    private ScottPlot.IColormap _Colormap { get; } = colormap;

    public string Name => _Colormap.Name;

    public override string ToString() => _Colormap.ToString();

    public static Colormap[] GetColormaps() => ScottPlot.Colormap.GetColormaps()
        .Select(x => new Colormap(x))
        .ToArray();

    public static string[] GetColormapNames() => ScottPlot.Colormap.GetColormaps()
        .Select(x => new Colormap(x).Name)
        .ToArray();

    public static Colormap GetColormap(string colormapName)
    {
        foreach (Colormap cmap in GetColormaps())
            if (string.Equals(cmap.Name, colormapName, StringComparison.InvariantCultureIgnoreCase))
                return cmap;

        throw new ArgumentException($"Colormap does not exist: {colormapName}");
    }

    public (byte r, byte g, byte b) GetRGB(byte value) => GetRGB(value / 255.0);

    public (byte r, byte g, byte b) GetRGB(double fraction)
    {
        ScottPlot.Color color = _Colormap.GetColor(fraction);
        return (color.R, color.G, color.B);
    }

    public int GetInt32(byte value)
    {
        var (r, g, b) = GetRGB(value);
        return 255 << 24 | r << 16 | g << 8 | b;
    }

    public int GetInt32(double fraction)
    {
        var (r, g, b) = GetRGB(fraction);
        return 255 << 24 | r << 16 | g << 8 | b;
    }

    public SKColor GetColor(byte value)
    {
        var color = GetInt32(value);
        return new SKColor((uint)color);
    }

    public SKColor GetColor(double fraction)
    {
        var color = GetInt32(fraction);
        return new SKColor((uint)color);
    }

    public SKBitmap ApplyFilter(SKBitmap bmp)
    {
        SKImageInfo info = new(bmp.Width, bmp.Height, SKColorType.Rgba8888);
        SKBitmap newBitmap = new(info);
        using SKCanvas canvas = new(newBitmap);
        canvas.Clear();

        using SKPaint paint = new SKPaint();

        byte[] A = new byte[256];
        byte[] R = new byte[256];
        byte[] G = new byte[256];
        byte[] B = new byte[256];

        for (int i = 0; i < 256; i++)
        {
            var color = GetColor((byte)i);
            A[i] = color.Alpha;
            R[i] = color.Red;
            G[i] = color.Green;
            B[i] = color.Blue;
        }
        paint.ColorFilter = SKColorFilter.CreateTable(A, R, G, B);

        canvas.DrawBitmap(bmp, 0, 0, paint);
        return newBitmap;
    }
}
