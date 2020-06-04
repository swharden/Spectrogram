using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArgoNot
{
    public class WsprSpot
    {
        public readonly bool isValid;
        public readonly DateTime dt;
        public double age { get { return (DateTime.UtcNow - dt).TotalMinutes; } }
        public int segment { get { return (dt.Minute % 10) / 2; } }
        public readonly string timestamp;
        public readonly double strength;
        public readonly double deltaT;
        public readonly double frequencyMHz;
        public readonly int frequencyHz;
        public readonly string grid;
        public readonly double power;
        public readonly double drift;
        public readonly string callsign;

        public WsprSpot(string line)
        {
            string[] parts = line?.Trim().Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();

            try
            {
                // 0      1    2   3      4           5     6    7            8  9     10 11   12 13 14     15  16
                // 200603 1720 -27 -1.34  10.1401891  N8XEF EM99 37           3  0.13  1  1    0  1  38     1   810

                // index 0 and 1: date and time (UTC)
                int year = int.Parse(parts[0].Substring(0, 2)) + 2000;
                int month = int.Parse(parts[0].Substring(2, 2));
                int day = int.Parse(parts[0].Substring(4, 2));
                int hour = int.Parse(parts[1].Substring(0, 2));
                int minute = int.Parse(parts[1].Substring(2, 2));
                int second = 0;
                dt = new DateTime(year, month, day, hour, minute, second);
                timestamp = parts[0] + parts[1];

                // index 2: signal strength (dB)
                strength = int.Parse(parts[2]);

                // index 3: time error (sec)
                deltaT = double.Parse(parts[3]);

                // index 4: frequency (MHz)
                frequencyMHz = double.Parse(parts[4]);
                frequencyHz = (int)(frequencyMHz * 1e6);

                // index 5: call sign
                callsign = parts[5];

                // index 6: grid square
                grid = parts[6];

                // index 7: power (dB)
                power = double.Parse(parts[7]);

                // index 8: drift (Hz)
                drift = double.Parse(parts[7]);

                isValid = true;
            }
            catch
            {
                isValid = false;
            }
        }

        public override string ToString()
        {
            return (isValid) ? $"{callsign} ({strength} dB) from {grid} at {dt}" : "invalid";
        }
    }
}
