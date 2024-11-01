using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using SkiaSharp;

namespace Spectrogram
{
    public class Colormap
    {
        public static Colormap Argo => new Colormap(new Colormaps.Argo());
        public static Colormap Blues => new Colormap(new Colormaps.Blues());
        public static Colormap Grayscale => new Colormap(new Colormaps.Grayscale());
        public static Colormap GrayscaleReversed => new Colormap(new Colormaps.GrayscaleR());
        public static Colormap Greens => new Colormap(new Colormaps.Greens());
        public static Colormap Inferno => new Colormap(new Colormaps.Inferno());
        public static Colormap Lopora => new Colormap(new Colormaps.Lopora());
        public static Colormap Magma => new Colormap(new Colormaps.Magma());
        public static Colormap Plasma => new Colormap(new Colormaps.Plasma());
        public static Colormap Turbo => new Colormap(new Colormaps.Turbo());
        public static Colormap Viridis => new Colormap(new Colormaps.Viridis());

        private readonly IColormap cmap;
        public readonly string Name;
        public Colormap(IColormap colormap)
        {
            cmap = colormap ?? new Colormaps.Grayscale();
            Name = cmap.GetType().Name;
        }

        public override string ToString()
        {
            return $"Colormap {Name}";
        }

        public static Colormap[] GetColormaps() =>
            typeof(Colormap).GetProperties()
                            .Select(x => (Colormap)x.GetValue(x.Name))
                            .ToArray();

        public static string[] GetColormapNames()
        {
            return GetColormaps().Select(x => x.Name).ToArray();
        }

        public static Colormap GetColormap(string colormapName)
        {
            foreach (Colormap cmap in GetColormaps())
                if (string.Equals(cmap.Name, colormapName, StringComparison.InvariantCultureIgnoreCase))
                    return cmap;

            throw new ArgumentException($"Colormap does not exist: {colormapName}");
        }

        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            return cmap.GetRGB(value);
        }

        public (byte r, byte g, byte b) GetRGB(double fraction)
        {
            fraction = Math.Max(fraction, 0);
            fraction = Math.Min(fraction, 1);
            return cmap.GetRGB((byte)(fraction * 255));
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
            var color =  GetInt32(value);
            return new SKColor((uint)color);
        }

        public SKColor GetColor(double fraction)
        {
            var color = GetInt32(fraction);
            return new SKColor((uint)color);
        }

        public SKBitmap ApplyFilter(SKBitmap bmp)
        {
            SKImageInfo info = new SKImageInfo(bmp.Width, bmp.Height, SKColorType.Rgba8888);
            SKBitmap newBitmap = new SKBitmap(info);
            using SKCanvas canvas = new SKCanvas(newBitmap);
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
}
