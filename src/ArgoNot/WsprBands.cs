using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ArgoNot
{
    public class WsprBand
    {
        public readonly string name;
        public readonly int dialFreq;
        public readonly int lowerFreq;
        public readonly int upperFreq;
        public WsprBand(string name, int dial, int low, int high)
        {
            this.name = name ?? $"?";
            dialFreq = dial;
            lowerFreq = low;
            upperFreq = high;
        }
    }

    public static class WsprBands
    {
        public static WsprBand[] GetBands(bool ascending = true)
        {
            // http://wsprnet.org/drupal/node/218

            WsprBand[] bands =
            {
                new WsprBand("None", 0, 0, 0),
                new WsprBand("160m", 1836600, 1838000, 1838200),
                new WsprBand("80m", 3592600, 3594000, 3594200),
                new WsprBand("60m", 5287200, 5288600, 5288800),
                new WsprBand("40m", 7038600, 7040000, 7040200),
                new WsprBand("30m", 10138700, 10140100, 10140300),
                new WsprBand("20m", 14095600, 14097000, 14097200),
                new WsprBand("17m", 18104600, 18106000, 18106200),
                new WsprBand("15m", 21094600, 21096000, 21096200),
                new WsprBand("12m", 24924600, 24926000, 24926200),
                new WsprBand("10m", 28124600, 28126000, 28126200),
                new WsprBand("6m", 50293000, 50294400, 50294600),
                new WsprBand("2m", 144488500, 144489900, 144490100)
            };

            if (ascending == false)
                Array.Reverse(bands);

            return bands;
        }
    }
}
