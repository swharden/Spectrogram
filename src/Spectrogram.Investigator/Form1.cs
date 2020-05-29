using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectrogram.Investigator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cbFftSize.SelectedIndex = 4;

            string sampleFilePath = System.IO.Path.GetFullPath("../../../../../data/cant-do-that.mp3");
            if (System.IO.File.Exists(sampleFilePath))
                LoadMp3(sampleFilePath);
        }

        Spectrogram spec;
        private void LoadMp3(string filePath)
        {
            (double[] audio, int sampleRate) = Read.MP3(filePath);
            int fftSize = int.Parse(cbFftSize.Text);
            spec = new Spectrogram(audio, sampleRate, fftSize, )
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {

        }
    }
}
