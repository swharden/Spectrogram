using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Spectrogram
{
    public class Benchmark : IDisposable
    {
        Stopwatch stopwatch;

        public double elapsedMilliseconds { get { return stopwatch.ElapsedTicks * 1000.0 / Stopwatch.Frequency; } }

        public Benchmark()
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            Console.WriteLine(string.Format("completed in {0:0.00} ms", elapsedMilliseconds));
        }

    }
}
