using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArgoNot
{
    public class WsprLogWatcher
    {
        public readonly string filePath;
        public readonly List<WsprSpot> allSpots = new List<WsprSpot>();

        public WsprLogWatcher(string filePath)
        {
            this.filePath = Path.GetFullPath(filePath);
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var streamReader = new StreamReader(stream))
            {
                while (!streamReader.EndOfStream)
                {
                    var spot = new WsprSpot(streamReader.ReadLine());
                    if (spot.isValid)
                        allSpots.Add(spot);
                }
            }
        }
    }
}
