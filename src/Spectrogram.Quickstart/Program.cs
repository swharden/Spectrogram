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
            // Get sample audio by decoding a MP3 file
            string audioFilePath = "../../../../../data/cant-do-that.mp3";
            (double[] audio, int sampleRate) = Read.MP3(audioFilePath, scale: false);

            // frequency analysis is performed on initialization
            var spec = new Spectrogram(
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
