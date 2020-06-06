using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArgoNot
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Compress(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void Compress(int factor)
        {

            Enabled = false;

            string filePath = @"C:\Users\Scott\Documents\temp\wspr-48b.mp3";
            (double[] audio, int sampleRate) = Spectrogram.Read.MP3(filePath);
            sampleRate = 48000;
            int fftSize = 1 << 15 + factor;
            Console.WriteLine($"FFT Size: {fftSize}");

            int widthPx = 1000;
            int stepSize = sampleRate * 60 * 10 / widthPx; // 10 minutes

            double[] window = FftSharp.Window.Hanning(1 << 16);

            var spec = new Spectrogram.SpectrogramImage(audio, sampleRate, fftSize, stepSize,
                freqMin: 1200, freqMax: 1500, multiplier: .01, window: window);

            spec.cmap = new Spectrogram.Colormaps.Grayscale();

            spec.SaveBMPcompressed($"compressed-{factor}-{fftSize}.bmp", factor - 1);
            Console.WriteLine("DONE");

            Enabled = true;
        }
    }
}
