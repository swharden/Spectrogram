using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var spec = new Spectrogram.Spectrogram();

            double[] values = Spectrogram.Tools.generateFakeSignal();
            double[] fft = Spectrogram.Tools.FFT(values);

            Spectrogram.Tools.plotValues(values);
            Spectrogram.Tools.plotFFT(fft);

            Console.WriteLine("DONE");
        }
    }
}
