using Spectrogram.Colormaps;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;

namespace Spectrogram.Quickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            (double[] sampleAudio, _) = Read.WavInt16mono(@"C:\Users\Scott\Documents\important\data\200605-6k.wav");
            int sampleRate = 6000;

            int fftSize = 1 << 15;
            int windowSize = fftSize;
            int stepSize = (sampleAudio.Length - windowSize) / 1000;

            var spec = new Spectrogram2(sampleRate, fftSize, windowSize, stepSize, 1200, 1500);

            spec.Add(sampleAudio);

            Console.WriteLine("processing...");
            spec.Process();
            Console.WriteLine(spec);

            Console.WriteLine("writing bitmap...");
            Bitmap bmp = spec.GetBitmap();
            bmp.Save("output.bmp");
        }

        static void Quickstart()
        {
            // Get sample audio by decoding a MP3 file
            string audioFilePath = "../../../../../data/cant-do-that.mp3";
            (double[] audio, int sampleRate) = Read.MP3(audioFilePath);

            // frequency analysis is performed on initialization
            var spec = new SpectrogramImage(
                    signal: audio,
                    sampleRate: sampleRate,
                    fftSize: 4096,
                    stepSize: 500,
                    freqMax: 2_500,
                    multiplier: .03,
                    dB: false
                );

            // you can save the output
            spec.SaveJPG("output.jpg");

            // or get it as a Bitmap you can use in a GUI
            Bitmap bmp = spec.GetBitmap();

            // pixel intensities can be recalculated using different settings
            spec.Recalculate(
                    multiplier: 2.8,
                    dB: true,
                    cmap: new Inferno()
                );
            spec.SaveJPG("output2.jpg");

            Console.WriteLine($"Saved in: {System.IO.Path.GetFullPath("./")}");
        }
    }
}
