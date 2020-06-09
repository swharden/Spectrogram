using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Spectrogram
{
    public class Colormap
    {
        public static Colormap Afmhot => new Colormap(new Colormaps.Afmhot());
        public static Colormap AfmhotReversed => new Colormap(new Colormaps.AfmhotReversed());
        public static Colormap Argo => new Colormap(new Colormaps.Argo());
        public static Colormap Autumn => new Colormap(new Colormaps.Autumn());
        public static Colormap AutumnReversed => new Colormap(new Colormaps.AutumnReversed());
        public static Colormap Binary => new Colormap(new Colormaps.Binary());
        public static Colormap BinaryReversed => new Colormap(new Colormaps.BinaryReversed());
        public static Colormap Blues => new Colormap(new Colormaps.Blues());
        public static Colormap BluesReversed => new Colormap(new Colormaps.BluesReversed());
        public static Colormap Bone => new Colormap(new Colormaps.Bone());
        public static Colormap BoneReversed => new Colormap(new Colormaps.BoneReversed());
        public static Colormap Brbg => new Colormap(new Colormaps.Brbg());
        public static Colormap BrbgReversed => new Colormap(new Colormaps.BrbgReversed());
        public static Colormap Brg => new Colormap(new Colormaps.Brg());
        public static Colormap BrgReversed => new Colormap(new Colormaps.BrgReversed());
        public static Colormap Bugn => new Colormap(new Colormaps.Bugn());
        public static Colormap BugnReversed => new Colormap(new Colormaps.BugnReversed());
        public static Colormap Bupu => new Colormap(new Colormaps.Bupu());
        public static Colormap BupuReversed => new Colormap(new Colormaps.BupuReversed());
        public static Colormap Bwr => new Colormap(new Colormaps.Bwr());
        public static Colormap BwrReversed => new Colormap(new Colormaps.BwrReversed());
        public static Colormap Cividis => new Colormap(new Colormaps.Cividis());
        public static Colormap CividisReversed => new Colormap(new Colormaps.CividisReversed());
        public static Colormap Cmrmap => new Colormap(new Colormaps.Cmrmap());
        public static Colormap CmrmapReversed => new Colormap(new Colormaps.CmrmapReversed());
        public static Colormap Cool => new Colormap(new Colormaps.Cool());
        public static Colormap CoolReversed => new Colormap(new Colormaps.CoolReversed());
        public static Colormap Coolwarm => new Colormap(new Colormaps.Coolwarm());
        public static Colormap CoolwarmReversed => new Colormap(new Colormaps.CoolwarmReversed());
        public static Colormap Copper => new Colormap(new Colormaps.Copper());
        public static Colormap CopperReversed => new Colormap(new Colormaps.CopperReversed());
        public static Colormap Cubehelix => new Colormap(new Colormaps.Cubehelix());
        public static Colormap CubehelixReversed => new Colormap(new Colormaps.CubehelixReversed());
        public static Colormap Flag => new Colormap(new Colormaps.Flag());
        public static Colormap FlagReversed => new Colormap(new Colormaps.FlagReversed());
        public static Colormap GistReversedainbow => new Colormap(new Colormaps.GistReversedainbow());
        public static Colormap GistReversedainbowReversed => new Colormap(new Colormaps.GistReversedainbowReversed());
        public static Colormap Gnbu => new Colormap(new Colormaps.Gnbu());
        public static Colormap GnbuReversed => new Colormap(new Colormaps.GnbuReversed());
        public static Colormap Gnuplot => new Colormap(new Colormaps.Gnuplot());
        public static Colormap Gnuplot2 => new Colormap(new Colormaps.Gnuplot2());
        public static Colormap Gnuplot2Reversed => new Colormap(new Colormaps.Gnuplot2Reversed());
        public static Colormap GnuplotReversed => new Colormap(new Colormaps.GnuplotReversed());
        public static Colormap Gray => new Colormap(new Colormaps.Gray());
        public static Colormap GrayReversed => new Colormap(new Colormaps.GrayReversed());
        public static Colormap Greens => new Colormap(new Colormaps.Greens());
        public static Colormap GreensReversed => new Colormap(new Colormaps.GreensReversed());
        public static Colormap Greys => new Colormap(new Colormaps.Greys());
        public static Colormap GreysReversed => new Colormap(new Colormaps.GreysReversed());
        public static Colormap Hot => new Colormap(new Colormaps.Hot());
        public static Colormap HotReversed => new Colormap(new Colormaps.HotReversed());
        public static Colormap Hsv => new Colormap(new Colormaps.Hsv());
        public static Colormap HsvReversed => new Colormap(new Colormaps.HsvReversed());
        public static Colormap Inferno => new Colormap(new Colormaps.Inferno());
        public static Colormap InfernoReversed => new Colormap(new Colormaps.InfernoReversed());
        public static Colormap Jet => new Colormap(new Colormaps.Jet());
        public static Colormap JetReversed => new Colormap(new Colormaps.JetReversed());
        public static Colormap Magma => new Colormap(new Colormaps.Magma());
        public static Colormap MagmaReversed => new Colormap(new Colormaps.MagmaReversed());
        public static Colormap Ocean => new Colormap(new Colormaps.Ocean());
        public static Colormap OceanReversed => new Colormap(new Colormaps.OceanReversed());
        public static Colormap Oranges => new Colormap(new Colormaps.Oranges());
        public static Colormap OrangesReversed => new Colormap(new Colormaps.OrangesReversed());
        public static Colormap Orrd => new Colormap(new Colormaps.Orrd());
        public static Colormap OrrdReversed => new Colormap(new Colormaps.OrrdReversed());
        public static Colormap Pink => new Colormap(new Colormaps.Pink());
        public static Colormap PinkReversed => new Colormap(new Colormaps.PinkReversed());
        public static Colormap Piyg => new Colormap(new Colormaps.Piyg());
        public static Colormap PiygReversed => new Colormap(new Colormaps.PiygReversed());
        public static Colormap Plasma => new Colormap(new Colormaps.Plasma());
        public static Colormap PlasmaReversed => new Colormap(new Colormaps.PlasmaReversed());
        public static Colormap Prgn => new Colormap(new Colormaps.Prgn());
        public static Colormap PrgnReversed => new Colormap(new Colormaps.PrgnReversed());
        public static Colormap PrismReversed => new Colormap(new Colormaps.PrismReversed());
        public static Colormap Pubu => new Colormap(new Colormaps.Pubu());
        public static Colormap PubuReversed => new Colormap(new Colormaps.PubuReversed());
        public static Colormap Pubugn => new Colormap(new Colormaps.Pubugn());
        public static Colormap PubugnReversed => new Colormap(new Colormaps.PubugnReversed());
        public static Colormap Puor => new Colormap(new Colormaps.Puor());
        public static Colormap PuorReversed => new Colormap(new Colormaps.PuorReversed());
        public static Colormap Purd => new Colormap(new Colormaps.Purd());
        public static Colormap PurdReversed => new Colormap(new Colormaps.PurdReversed());
        public static Colormap Purples => new Colormap(new Colormaps.Purples());
        public static Colormap PurplesReversed => new Colormap(new Colormaps.PurplesReversed());
        public static Colormap Rainbow => new Colormap(new Colormaps.Rainbow());
        public static Colormap RainbowReversed => new Colormap(new Colormaps.RainbowReversed());
        public static Colormap Rdbu => new Colormap(new Colormaps.Rdbu());
        public static Colormap RdbuReversed => new Colormap(new Colormaps.RdbuReversed());
        public static Colormap Rdgy => new Colormap(new Colormaps.Rdgy());
        public static Colormap RdgyReversed => new Colormap(new Colormaps.RdgyReversed());
        public static Colormap Rdpu => new Colormap(new Colormaps.Rdpu());
        public static Colormap RdpuReversed => new Colormap(new Colormaps.RdpuReversed());
        public static Colormap Rdylbu => new Colormap(new Colormaps.Rdylbu());
        public static Colormap RdylbuReversed => new Colormap(new Colormaps.RdylbuReversed());
        public static Colormap Rdylgn => new Colormap(new Colormaps.Rdylgn());
        public static Colormap RdylgnReversed => new Colormap(new Colormaps.RdylgnReversed());
        public static Colormap Reds => new Colormap(new Colormaps.Reds());
        public static Colormap RedsReversed => new Colormap(new Colormaps.RedsReversed());
        public static Colormap Seismic => new Colormap(new Colormaps.Seismic());
        public static Colormap SeismicReversed => new Colormap(new Colormaps.SeismicReversed());
        public static Colormap Spectral => new Colormap(new Colormaps.Spectral());
        public static Colormap SpectralReversed => new Colormap(new Colormaps.SpectralReversed());
        public static Colormap Spring => new Colormap(new Colormaps.Spring());
        public static Colormap SpringReversed => new Colormap(new Colormaps.SpringReversed());
        public static Colormap Summer => new Colormap(new Colormaps.Summer());
        public static Colormap SummerReversed => new Colormap(new Colormaps.SummerReversed());
        public static Colormap Terrain => new Colormap(new Colormaps.Terrain());
        public static Colormap TerrainReversed => new Colormap(new Colormaps.TerrainReversed());
        public static Colormap Twilight => new Colormap(new Colormaps.Twilight());
        public static Colormap TwilightReversed => new Colormap(new Colormaps.TwilightReversed());
        public static Colormap Viridis => new Colormap(new Colormaps.Viridis());
        public static Colormap ViridisReversed => new Colormap(new Colormaps.ViridisReversed());
        public static Colormap Winter => new Colormap(new Colormaps.Winter());
        public static Colormap WinterReversed => new Colormap(new Colormaps.WinterReversed());
        public static Colormap Wistia => new Colormap(new Colormaps.Wistia());
        public static Colormap WistiaReversed => new Colormap(new Colormaps.WistiaReversed());
        public static Colormap Ylgn => new Colormap(new Colormaps.Ylgn());
        public static Colormap YlgnReversed => new Colormap(new Colormaps.YlgnReversed());
        public static Colormap Ylgnbu => new Colormap(new Colormaps.Ylgnbu());
        public static Colormap YlgnbuReversed => new Colormap(new Colormaps.YlgnbuReversed());
        public static Colormap Ylorbr => new Colormap(new Colormaps.Ylorbr());
        public static Colormap YlorbrReversed => new Colormap(new Colormaps.YlorbrReversed());
        public static Colormap Ylorrd => new Colormap(new Colormaps.Ylorrd());
        public static Colormap YlorrdReversed => new Colormap(new Colormaps.YlorrdReversed());

        private readonly IColormap cmap;
        public readonly string Name;
        public Colormap(IColormap colormap)
        {
            cmap = colormap ?? new Colormaps.Gray();
            Name = cmap.GetType().Name;
        }

        public override string ToString()
        {
            return $"Colormap {Name}";
        }

        public static Colormap[] GetColormaps()
        {
            IColormap[] ics = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(s => s.GetTypes())
                                .Where(p => p.IsInterface == false)
                                .Where(p => p.ToString().StartsWith("Spectrogram.Colormaps."))
                                .Select(x => x.ToString())
                                .Select(path => (IColormap)Activator.CreateInstance(Type.GetType(path)))
                                .ToArray();

            return ics.Select(x => new Colormap(x)).ToArray();
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

        public Color GetColor(byte value)
        {
            return Color.FromArgb(GetInt32(value));
        }

        public Color GetColor(double fraction)
        {
            return Color.FromArgb(GetInt32(fraction));
        }

        public void Apply(Bitmap bmp)
        {
            System.Drawing.Imaging.ColorPalette pal = bmp.Palette;
            for (int i = 0; i < 256; i++)
                pal.Entries[i] = GetColor((byte)i);
            bmp.Palette = pal;
        }
    }
}
