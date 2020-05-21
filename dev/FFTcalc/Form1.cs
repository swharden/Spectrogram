using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFTcalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            cbRate.SelectedIndex = cbRate.Items.Count - 1;
            cbLengthMult.SelectedIndex = 0;

            for (int i = 5; i < 15; i++)
                cbFftSize.Items.Add($"2^{i} ({Math.Pow(2, i):N0})");
            cbFftSize.SelectedIndex = 5;
        }

        private void UpdateAll()
        {
            double sampleRate = (double.Parse(cbRate.Text.Replace("k", "")) * 1000);

            double lengthSec;
            if (cbLengthMult.Text == "sec" || string.IsNullOrWhiteSpace(cbLengthMult.Text))
                lengthSec = (double)nudLength.Value * 1;
            else if (cbLengthMult.Text == "min")
                lengthSec = (double)nudLength.Value * 60;
            else if (cbLengthMult.Text == "hr")
                lengthSec = (double)nudLength.Value * 60 * 60;
            else if (cbLengthMult.Text == "day")
                lengthSec = (double)nudLength.Value * 60 * 60 * 24;
            else
                throw new ArgumentException("unknown length multiplier");
            double lengthSamples = lengthSec * sampleRate;
            lblSamples.Text = $"{lengthSamples:N0}";

            int fftBase = cbFftSize.SelectedIndex + 5;
            int fftSize = (int)Math.Pow(2, fftBase);
            int fftHeightPx = fftSize;

            double maxFreq = sampleRate / 2;
            lblMaxFreq.Text = $"{maxFreq:N0} Hz";

            double fftRowHz = maxFreq / fftSize;
            double fftColSec = fftSize / sampleRate * 2; // I think *2?
            lblFftHeight.Text = $"{fftHeightPx:N0} px";
            lblFftResolution.Text = $"{fftRowHz:N0} Hz / row";
            lblFftTime.Text = $"{fftColSec * 1000:N2} ms";

            double fftStepSec = (double)nudStepMsec.Value / 1000;
            int fftStepSamples = (int)(fftStepSec * sampleRate);

            double stepFrac = fftStepSec / fftColSec;
            double overlapFrac = 1 - stepFrac;
            lblOverlap.Text = $"{overlapFrac * 100:N1} %";
            if (overlapFrac <= 0)
                lblOverlap.Text = "no overlap";

            int stepsInAudioFile = (int)((lengthSamples - fftSize) / fftStepSamples);
            int fftWidthPx = stepsInAudioFile;
            lblSpecWidth.Text = $"{fftWidthPx:N0} px";
            lblSpecHeight.Text = $"{fftHeightPx:N0} px";
        }

        private void cbRate_SelectedIndexChanged(object sender, EventArgs e) => UpdateAll();
        private void nudRate_ValueChanged(object sender, EventArgs e) => UpdateAll();
        private void nudLength_ValueChanged(object sender, EventArgs e) => UpdateAll();
        private void cbLengthMult_SelectedIndexChanged(object sender, EventArgs e) => UpdateAll();
        private void cbFftSize_SelectedIndexChanged(object sender, EventArgs e) => UpdateAll();
        private void nudOverlap_ValueChanged(object sender, EventArgs e) => UpdateAll();
    }
}
