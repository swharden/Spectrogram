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
            var spec = new Spectrogram.Spectrogram(fftSize: 1024, stepSize: 100_000);
            float[] values = Spectrogram.WavFile.Read(@"C:\Users\scott\Documents\temp\megaDrive.Wav");
            spec.SignalExtend(values);
            spec.SaveBitmap("megaDrive.jpg");
        }
    }
}
