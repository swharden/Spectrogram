using System;
using System.Collections.Generic;
using System.Text;

namespace Spectrogram
{
    public static class Colormap
    {
        public enum Name
        {
            Grayscale,
            Viridis,
            Cividis,
            Inferno,
            Magma,
            Plasma
        }

        public static Colormaps.Colormap GetColormap(Name name = Name.Viridis)
        {
            switch (name)
            {
                case Name.Grayscale:
                    return new Colormaps.Grayscale();
                case Name.Viridis:
                    return new Colormaps.Viridis();
                case Name.Cividis:
                    return new Colormaps.Cividis();
                case Name.Inferno:
                    return new Colormaps.Inferno();
                case Name.Magma:
                    return new Colormaps.Magma();
                case Name.Plasma:
                    return new Colormaps.Plasma();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
