using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRSS_Viewer
{
    public partial class FormConfig : Form
    {
        readonly Spectrogram.Settings.DisplaySettings displaySettings;

        public FormConfig(Spectrogram.Settings.DisplaySettings displaySettings)
        {
            InitializeComponent();
            this.displaySettings = displaySettings;
            UpdateGuiFromSettings();
        }

        private void FormBrightness_Load(object sender, EventArgs e)
        {

        }

        private void UpdateGuiFromSettings()
        {
            nudBrightness.Value = (decimal)displaySettings.brightness;
            trackBrightness.Value = (int)(displaySettings.brightness * 10);
            nudLower.Value = (decimal)displaySettings.freqLow;
            nudUpper.Value = (decimal)displaySettings.freqHigh;
        }

        private void NudBrightness_ValueChanged(object sender, EventArgs e)
        {
            displaySettings.brightness = (float)nudBrightness.Value;
            displaySettings.renderNeeded = true;
            UpdateGuiFromSettings();
        }

        private void TrackBrightness_Scroll(object sender, EventArgs e)
        {
            displaySettings.brightness = (float)trackBrightness.Value / 10;
            displaySettings.renderNeeded = true;
            UpdateGuiFromSettings();
        }

        private void BtnAuto_Click(object sender, EventArgs e)
        {
            displaySettings.renderNeeded = true;
        }

        private void NudLower_ValueChanged(object sender, EventArgs e)
        {
            displaySettings.freqLow = (double)nudLower.Value;
            displaySettings.renderNeeded = true;
            UpdateGuiFromSettings();
        }

        private void NudUpper_ValueChanged(object sender, EventArgs e)
        {
            displaySettings.freqHigh = (double)nudUpper.Value;
            displaySettings.renderNeeded = true;
            UpdateGuiFromSettings();
        }
    }
}
