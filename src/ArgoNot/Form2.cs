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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int sampleRate = 6000;
            var audio = FftSharp.SampleData.WhiteNoise(sampleRate * 60 * 10);

            int widthPx = 1000;
            int stepSize = sampleRate * 60 * 10 / widthPx; // 10 minutes

            var spec = new Spectrogram.SpectrogramLive(sampleRate, 1 << 13, stepSize, widthPx);

            var grab = new Grab(spec, 1100, 1500);
            Bitmap bmp = grab.GetBitmap();
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bmp;
            pictureBox1.Size = bmp.Size;
        }
    }
}
